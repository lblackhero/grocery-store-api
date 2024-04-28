using AutoMapper;
using GroceryStore.Application.Interfaces.Grocery.Order;
using GroceryStore.Application.Interfaces.Grocery.Stock;
using GroceryStore.Common.DTOS.Grocery.Order;
using GroceryStore.Common.Functionalities;
using GroceryStore.Common.Models.Common.GlobalResponse;
using GroceryStore.Common.Models.Grocery.Order;
using GroceryStore.Common.Models.Grocery.Stock;
using GroceryStore.Common.Statics.Common;
using GroceryStore.Domain.Entities.Grocery.Order;
using GroceryStore.Domain.Entities.Grocery.Products;
using GroceryStore.Domain.Entities.Identity;
using GroceryStore.Infraestructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GroceryStore.Logic.Repositories.Grocery.Orders;

public class OrderRepository(DatabaseContextGroceryStore databaseContextGroceryStore,
							 IStockRepository stockRepository,
							 IFunctionalities functionalities,
							 IMapper mapper) : IOrderRepository
{
	private readonly DatabaseContextGroceryStore DatabaseContextGroceryStore = databaseContextGroceryStore;
	private readonly IStockRepository StockRepository = stockRepository;
	private readonly IFunctionalities Functionalities = functionalities;
	private readonly IMapper Mapper = mapper;

	#region Post Methods
	/// <summary>
	/// Se encarga de gestionar todos los procesos relacionados
	/// que se ejecutan al realizar una orden
	/// </summary>
	/// <param name="userOrder">Orden del usuario</param>
	/// <param name="userNameIdentifier">Id del usuario</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> BuyProducts(List<OrderDetailModel> userOrder, string userNameIdentifier)
	{
		IAsyncEnumerable<ProductEntity> productsInStock = DatabaseContextGroceryStore.GetAvailableProducts();
		List<OrderDetailModel> productsData = [];
		List<StockModel> stocksData = [];

		await foreach (ProductEntity product in productsInStock.ConfigureAwait(false))
		{
			productsData.Add(new(product.ProductId, product.Price, product.Stock.Quantity, 0));
			stocksData.Add(new(product.Stock.ProductId, product.Stock.Quantity, product.Stock.CreationDate));
		}

		ReturnResponses consistencyErrors = CheckOrderDataConsistency(userOrder, productsData);

		if (consistencyErrors.StatusCode != HttpStatusCode.OK)
			return consistencyErrors;

		OrderProcessingDto orderProcessingDto = new(userOrder, productsData, stocksData);
		ReturnResponses orderProcessingTask = await ProcessOrderAsync(orderProcessingDto, userNameIdentifier);

		if (orderProcessingTask.StatusCode != HttpStatusCode.OK)
			return orderProcessingTask;

		object? orderId = orderProcessingTask.ResponseData?.GetType()?.GetProperty("ResponseData")?.GetValue(orderProcessingTask.ResponseData);

		return new(HttpStatusCode.OK, orderId, GenericResponse.GenericOkMessage);
	}

	/// <summary>
	/// Se encarga de guardar los datos de una orden
	/// </summary>
	/// <param name="order">Datos de la orden</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> CreateOrderAsync(OrderModel order)
	{
		OrderEntity orderEntity = Mapper.Map<OrderEntity>(order);

		try
		{
			var orderResult = await DatabaseContextGroceryStore.AddAsync(orderEntity);
			await DatabaseContextGroceryStore.SaveChangesAsync(true);

			return new(HttpStatusCode.OK, orderResult.Entity.OrderId, ResponseMessage: GenericResponse.GenericOkMessage);
		}
		catch
		{
			return new(HttpStatusCode.BadRequest, ResponseMessage: "An error ocurred while trying to create the order");
		}
	}
	#endregion Post Methods

	#region Get Methods
	/// <summary>
	/// Se encarga de crear el resumen de una orden
	/// </summary>
	/// <param name="orderId">Id de la orden</param>
	/// <param name="userNameIdentifier">Id del usuario</param>
	/// <returns>OrderSummaryModel</returns>
	public async Task<OrderSummaryDto> GetOrderSummary(Guid orderId, string userNameIdentifier)
	{
		if (orderId.Equals(Guid.Empty))
			return new();

		UserEntity orderSummary = await DatabaseContextGroceryStore.GetOrderSummaryByUserAndOrderId(userNameIdentifier, orderId);

		OrderSummaryDto summary = new(
			orderSummary.Email,
			orderSummary.FullName,
			orderSummary.Contact,
			orderSummary.Orders.ElementAt(0).OrderNumber,
			orderSummary.Orders.ElementAt(0).TotalToPay,
			orderSummary.Orders.ElementAt(0).CreationDate
		);

		IAsyncEnumerable<ProductEntity> orderDetailSummary = DatabaseContextGroceryStore.GetOrderDetailsSummaryByOrderId(orderId);

		await foreach (ProductEntity product in orderDetailSummary.ConfigureAwait(false))
			summary.Details.Add(new OrderDetailDto(product.Name,
												   product.Description,
												   product.Price,
												   product.OrderDetails.ElementAt(0).Quantity,
												   product.OrderDetails.ElementAt(0).Total));

		return summary;
	}
	#endregion Get Methods

	#region Utility Methods
	/// <summary>
	/// Se encarga de validar la consistencia de los datos de una orden
	/// </summary>
	/// <param name="userOrder">Orden con los datos ingresados por el usuario</param>
	/// <param name="availableProducts">Datos de los productos disponibles para comprar</param>
	/// <returns>ReturnResponses</returns>
	private static ReturnResponses CheckOrderDataConsistency(List<OrderDetailModel> userOrder, List<OrderDetailModel> availableProducts)
	{
		if (availableProducts.Count <= 0)
			return new(HttpStatusCode.BadRequest, null, GenericResponse.GenericNotFoundMessage);

		IEnumerable<Guid> nonExistingProductsGuids = from u in userOrder
													 where !(from a in availableProducts
															 select a.ProductId)
															 .Contains(u.ProductId)
													 select u.ProductId;

		if (nonExistingProductsGuids.Any())
			return new(HttpStatusCode.BadRequest, nonExistingProductsGuids, "The following product(s) does not exist(s) among the avialable products catalog");

		IEnumerable<Guid> excedeedStockProductsGuids = from u in userOrder
													   join a in availableProducts on u.ProductId equals a.ProductId
													   where a.Quantity < u.Quantity
													   select u.ProductId;

		if (excedeedStockProductsGuids.Any())
			return new(HttpStatusCode.BadRequest, excedeedStockProductsGuids, "The following product(s) exceed the available stock");

		IEnumerable<Guid> duplicatedProductsGuids = userOrder.Select(e => e.ProductId)
													.GroupBy(x => x).Where(z => z.Count() > 1).Select(e => e.Key);

		if (duplicatedProductsGuids.Any())
			return new(HttpStatusCode.BadRequest, duplicatedProductsGuids, "One or more products are duplicated");

		return new(HttpStatusCode.OK, GenericResponse.GenericOkMessage);
	}

	/// <summary>
	/// Se encarga de validar que los procesos de gestionar una orden
	/// y el stock restante sean exitosos
	/// </summary>
	/// <param name="orderProcessing">Datos de la orden</param>
	/// <param name="userNameIdentifier">Id del usuario</param>
	/// <returns>ReturnResponses</returns>
	private async Task<ReturnResponses> ProcessOrderAsync(OrderProcessingDto orderProcessing, string userNameIdentifier)
	{
		using var transaction = await DatabaseContextGroceryStore.Database.BeginTransactionAsync();

		try
		{
			ReturnResponses orderTask = await ManageOrderAsync(orderProcessing, userNameIdentifier);
			ReturnResponses stockTask = await ManageStockAsync(orderProcessing);

			if (orderTask.StatusCode != HttpStatusCode.OK)
			{
				await transaction.RollbackAsync();
				return orderTask;
			}
			else if (stockTask.StatusCode != HttpStatusCode.OK)
			{
				await transaction.RollbackAsync();
				return stockTask;
			}

			await transaction.CommitAsync();

			return new(HttpStatusCode.OK, orderTask, GenericResponse.GenericOkMessage);
		}
		catch
		{
			await transaction.RollbackAsync();
			return new(HttpStatusCode.BadRequest, ResponseMessage: "An error occurred while processing the order");
		}
	}

	/// <summary>
	/// Se encarga de gestionar una orden
	/// creando el modelo que sera guardado
	/// </summary>
	/// <param name="orderProcessing">Datos de la orden</param>
	/// <param name="userNameIdentifier">Id del usuario</param>
	/// <returns>ReturnResponses</returns>
	private async Task<ReturnResponses> ManageOrderAsync(OrderProcessingDto orderProcessing, string userNameIdentifier)
	{
		DateTime creationOrderDate = Functionalities.GetTimeColombia();

		List<OrderDetailModel> orderDetail = (from o in orderProcessing.UserOrder
											  join p in orderProcessing.AvailableProducts on o.ProductId equals p.ProductId
											  select new OrderDetailModel
											  {
												  ProductId = p.ProductId,
												  UnitPrice = p.UnitPrice,
												  Quantity = o.Quantity,
												  Total = p.UnitPrice * o.Quantity,
												  CreationDate = creationOrderDate,
											  }).ToList();

		OrderModel order = new()
		{
			UserId = new Guid(userNameIdentifier),
			TotalToPay = orderDetail.Sum(e => e.Total),
			Details = orderDetail,
			CreationDate = creationOrderDate
		};

		return await CreateOrderAsync(order);
	}

	/// <summary>
	/// Se encarga de gestionar el stock restante
	/// creando el modelo para actualizar el stock
	/// </summary>
	/// <param name="orderProcessing">Datos de la orden</param>
	/// <returns>ReturnResponses</returns>
	private async Task<ReturnResponses> ManageStockAsync(OrderProcessingDto orderProcessing)
	{
		IEnumerable<StockModel> stocksToUpdate = from o in orderProcessing.UserOrder
												 join s in orderProcessing.AvailableStock on o.ProductId equals s.ProductId
												 select new StockModel
												 {
													 ProductId = s.ProductId,
													 Quantity = s.Quantity - o.Quantity,
													 IsAvailable = (s.Quantity - o.Quantity) > 0,
													 CreationDate = s.CreationDate
												 };

		return await StockRepository.UpdateStocks(stocksToUpdate);
	}
	#endregion Utility Methods
}
