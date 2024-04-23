using GroceryStore.Domain.Entities.Grocery.Products;
using GroceryStore.Domain.Entities.Grocery.Stock;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infraestructure.DatabaseContext;

/// <summary>
/// Contexto de la base de datos
/// </summary>
/// <param name="options"></param>
public class DatabaseContextGroceryStore(DbContextOptions<DatabaseContextGroceryStore> options) : DbContext(options)
{
	#region Product Get Querys
	/// <summary>
	/// Query para buscar un producto con su stock
	/// </summary>
	static readonly Func<DatabaseContextGroceryStore, Guid, Task<ProductEntity?>> GetProductAndStockByIdQuery =
	EF.CompileAsyncQuery((DatabaseContextGroceryStore context, Guid id) =>
		 context.Products.Include(e => e.Stock).Where(e => e.ProductId.Equals(id)).AsNoTracking().FirstOrDefault());

	/// <summary>
	/// Obtiene el producto y su stock por id
	/// </summary>
	/// <param name="id">Id del producto</param>
	/// <returns>ProductEntity?</returns>
	public async Task<ProductEntity?> GetProductAndStockByIdAsync(Guid id)
		=> await GetProductAndStockByIdQuery(this, id);

	/// <summary>
	/// Query para buscar todos los productos y su stock asociado
	/// </summary>
	static readonly Func<DatabaseContextGroceryStore, IAsyncEnumerable<ProductEntity>> GetProductsAndStockQuery =
	EF.CompileAsyncQuery((DatabaseContextGroceryStore context) =>
		context.Products.Include(e => e.Stock).AsNoTracking());

	/// <summary>
	/// Obtiene todos los productos y su stock asociado
	/// </summary>
	/// <returns>ProductEntity</returns>
	public IAsyncEnumerable<ProductEntity> GetProductsAndStockAsync()
		=> GetProductsAndStockQuery(this);
	#endregion Product Get Querys

	#region Entities
	/// <summary>
	/// Entidad de productos
	/// </summary>
	public DbSet<ProductEntity> Products { get; set; }

	/// <summary>
	/// Entidad de stock/inventario
	/// </summary>
	public DbSet<StockEntity> Stocks { get; set; }
	#endregion Entities
}