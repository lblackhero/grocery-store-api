using AutoMapper;
using GroceryStore.Application.Interfaces.Grocery.Order;
using GroceryStore.Common.Models;
using GroceryStore.Common.Models.Grocery.Order;
using GroceryStore.Common.Statics;
using GroceryStore.Domain.Entities.Grocery.Products;
using GroceryStore.Infraestructure.DatabaseContext;
using System.Net;

namespace GroceryStore.Logic.Repositories.Grocery.Orders;

public class OrderRepository(DatabaseContextGroceryStore databaseContextGroceryStore) : IOrderRepository
{
	private readonly DatabaseContextGroceryStore DatabaseContextGroceryStore = databaseContextGroceryStore;

	#region Get Methods
	/// <summary>
	/// Se encarga de obtener los productos disponibles (con stock)
	/// </summary>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> GetAvailableProducts()
	{
		IAsyncEnumerable<ProductEntity> availableProducts = DatabaseContextGroceryStore.GetAvailableProducts();
		List<AvailableProductModel> products = [];

		await foreach (ProductEntity product in availableProducts.ConfigureAwait(false))
			products.Add(new (product.ProductId, product.Name, product.Description, product.Price, product.Stock.Quantity));

		if (products.Count <= 0)
			return new(HttpStatusCode.NotFound, null, GenericResponse.GenericNotFoundMessage);

		return new(HttpStatusCode.OK, products, GenericResponse.GenericOkMessage);
	}
	#endregion Get Methods

}
