using AutoMapper;
using GroceryStore.Application.Interfaces.Grocery.Products;
using GroceryStore.Common.Models;
using GroceryStore.Common.Models.Grocery.Product;
using GroceryStore.Common.Statics;
using GroceryStore.Domain.Entities.Grocery.Products;
using GroceryStore.Infraestructure.DatabaseContext;
using System.Net;

namespace GroceryStore.Logic.Repositories.Grocery.Products;

public class ProductRepository(DatabaseContextGroceryStore databaseContextGroceryStore, IMapper mapper) : IProductRepository
{
	private readonly DatabaseContextGroceryStore DatabaseContextGroceryStore = databaseContextGroceryStore;
	private readonly IMapper Mapper = mapper;

	#region Get Methods
	/// <summary>
	/// Obtiene un producto y su stock por id
	/// </summary>
	/// <param name="productId">Id del producto</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> GetProductAndStockByProductIdAsync(Guid productId)
	{
		ProductEntity? product = await DatabaseContextGroceryStore.GetProductAndStockByIdAsync(productId);

		if (product is null)
			return new(HttpStatusCode.NotFound, null, GenericResponse.GenericNotFoundMessage);

		return new(HttpStatusCode.OK, product, GenericResponse.GenericOkMessage);
	}

	/// <summary>
	/// Obtiene todos los productos con su stock
	/// </summary>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> GetProductsAndStockAsync()
	{
		IAsyncEnumerable<ProductEntity> databaseProducts = DatabaseContextGroceryStore.GetProductsAndStockAsync();
		List<ProductEntity> products = [];

		await foreach (ProductEntity product in databaseProducts.ConfigureAwait(false))
			products.Add(product);
		
		if (products.Count <= 0)
			return new(HttpStatusCode.NotFound, null, GenericResponse.GenericNotFoundMessage);

		return new(HttpStatusCode.OK, products, GenericResponse.GenericOkMessage);
	}
	#endregion Get Methods

	#region Post Methods
	/// <summary>
	/// Crea un nuevo producto
	/// </summary>
	/// <param name="product">Producto</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> AddProductAsync(ProductModel product)
	{
		ProductEntity productToAdd = Mapper.Map<ProductEntity>(product);

		productToAdd.SetCreationDate();
		productToAdd.Stock.SetCreationDate();
		productToAdd.Stock.UpdateStockFields(product.Stock.Quantity, product.Stock.Quantity > 0);

		var result = DatabaseContextGroceryStore.Add(productToAdd);

		await DatabaseContextGroceryStore.SaveChangesAsync(true);

		return new(HttpStatusCode.Created, result.Entity, GenericResponse.GenericOkMessage);
	}
	#endregion Post Methods

	#region Put Methods
	/// <summary>
	/// Se encarga de actualizar un producto
	/// </summary>
	/// <param name="productModelToUpdate">Datos del producto</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> UpdateProductByIdAsync(ProductModelToUpdate productModelToUpdate)
	{
		ProductEntity? product = await DatabaseContextGroceryStore.GetProductAndStockByIdAsync(productModelToUpdate.ProductId);

		if (product is null)
			return new(HttpStatusCode.NotFound, null, GenericResponse.GenericNotFoundMessage);

		product.SetUpdateDate();
		product.UpdateProductFields(productModelToUpdate.Name ?? product.Name,
			productModelToUpdate.Price ?? product.Price, productModelToUpdate.Description ?? product.Description);

		if (productModelToUpdate.Stock is not null)
		{
			product.Stock.SetUpdateDate();
			product.Stock.UpdateStockFields(productModelToUpdate.Stock.Quantity ?? product.Stock.Quantity,
				productModelToUpdate.Stock.Quantity.HasValue ? productModelToUpdate.Stock.Quantity > 0 : product.Stock.Quantity > 0);
		}

		var result = DatabaseContextGroceryStore.Update(product);
		await DatabaseContextGroceryStore.SaveChangesAsync(true);

		return new(HttpStatusCode.OK, result.Entity, GenericResponse.GenericOkMessage);
	}
	#endregion Put Methods

	#region Delete Methods
	/// <summary>
	/// Se encarga de eliminar un producto
	/// </summary>
	/// <param name="productId">Id del producto</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> DeleteProductByIdAsync(Guid productId)
	{
		ProductEntity? product = await DatabaseContextGroceryStore.GetProductAndStockByIdAsync(productId);

		if (product is null)
			return new(HttpStatusCode.NotFound, null, GenericResponse.GenericNotFoundMessage);

		DatabaseContextGroceryStore.Remove(product);
		await DatabaseContextGroceryStore.SaveChangesAsync(true);

		return new(HttpStatusCode.NoContent, ResponseMessage: GenericResponse.GenericOkMessage);
	}
	#endregion Delete Methods
}
