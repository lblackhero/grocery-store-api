using GroceryStore.Common.Models;
using GroceryStore.Common.Models.Grocery.Product;

namespace GroceryStore.Application.Interfaces.Grocery.Products;

public interface IProductRepository
{
	#region Get Methods
	/// <summary>
	/// Obtiene un producto y su stock por id
	/// </summary>
	/// <param name="productId">Id del producto</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> GetProductAndStockByProductIdAsync(Guid productId);

	/// <summary>
	/// Obtiene todos los productos con su stock
	/// </summary>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> GetProductsAndStockAsync();
	#endregion Get Methods

	#region Post Methods
	/// <summary>
	/// Crea un nuevo producto
	/// </summary>
	/// <param name="product">Producto</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> AddProductAsync(ProductModel product);
	#endregion Post Methods

	#region Put Methods
	/// <summary>
	/// Se encarga de actualizar un producto
	/// </summary>
	/// <param name="productModelToUpdate">Datos del producto</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> UpdateProductByIdAsync(ProductModelToUpdate productModelToUpdate);
	#endregion Put Methods

	#region Delete Methods
	/// <summary>
	/// Se encarga de eliminar un producto
	/// </summary>
	/// <param name="productId">Id del producto</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> DeleteProductByIdAsync(Guid productId);
	#endregion Delete Methods
}