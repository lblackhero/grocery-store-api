using GroceryStore.Domain.Entities.Grocery.Order;
using GroceryStore.Domain.Entities.Grocery.Products;
using GroceryStore.Domain.Entities.Grocery.Stock;
using GroceryStore.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infraestructure.DatabaseContext;

/// <summary>
/// Contexto de la base de datos
/// </summary>
/// <param name="options"></param>
public class DatabaseContextGroceryStore(DbContextOptions<DatabaseContextGroceryStore> options) : DbContext(options)
{
	#region Admin Methods
	#region Product Get Queries
	/// <summary>
	/// Query para buscar un producto con su stock
	/// </summary>
	static readonly Func<DatabaseContextGroceryStore, Guid, Task<ProductEntity?>> GetProductAndStockByIdQuery =
		EF.CompileAsyncQuery((DatabaseContextGroceryStore context, Guid id) =>
			context.Products.AsNoTracking().Include(e => e.Stock).Where(e => e.ProductId.Equals(id)).FirstOrDefault());

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
			context.Products.AsNoTracking().Include(e => e.Stock));

	/// <summary>
	/// Obtiene todos los productos y su stock asociado
	/// </summary>
	/// <returns>ProductEntity</returns>
	public IAsyncEnumerable<ProductEntity> GetProductsAndStockAsync()
		=> GetProductsAndStockQuery(this);
	#endregion Product Get Queries
	#endregion Admin Methods

	#region Normal User Methods
	#region Product Get Queries
	/// <summary>
	/// Query para buscar los productos disponibles
	/// </summary>
	static readonly Func<DatabaseContextGroceryStore, IAsyncEnumerable<ProductEntity>> GetAvailableProductsQuery =
		EF.CompileAsyncQuery((DatabaseContextGroceryStore context) =>
			context.Products.AsNoTracking().Include(e => e.Stock).Where(e => e.Stock.IsAvailable));

	/// <summary>
	/// Obtiene los productos disponibles
	/// </summary>
	/// <returns>ProductEntity</returns>
	public IAsyncEnumerable<ProductEntity> GetAvailableProducts()
		=> GetAvailableProductsQuery(this);
	#endregion Product Get Queries

	#region Order Get Queries
	/// <summary>
	/// Query para obtener el resumen de una orden
	/// </summary>
	static readonly Func<DatabaseContextGroceryStore, string, Guid, Task<UserEntity>> GetOrderSummaryQuery =
		EF.CompileAsyncQuery((DatabaseContextGroceryStore context, string userId, Guid orderId) =>
			(from u in context.Users.AsNoTracking()
			 join o in context.Orders on u.Id equals o.UserId
			 where u.Id.Equals(userId) && o.OrderId.Equals(orderId)
			 select new UserEntity
			 {
				 Email = u.Email,
				 FullName = u.FullName,
				 Contact = u.Contact,
				 Orders = new List<OrderEntity>() { new(orderId, userId, o.OrderNumber, o.TotalToPay, o.CreationDate) },
			 }).First());

	/// <summary>
	/// Obtiene el resumen de una orden
	/// </summary>
	/// <param name="userNameIdentifier">Id del usuario</param>
	/// <param name="orderId">Id de la orden</param>
	/// <returns>UserEntity?</returns>
	public async Task<UserEntity> GetOrderSummaryByUserAndOrderId(string userNameIdentifier, Guid orderId)
		=> await GetOrderSummaryQuery(this, userNameIdentifier, orderId);

	/// <summary>
	/// Query para obtener los detalles de una orden
	/// </summary>
	static readonly Func<DatabaseContextGroceryStore, Guid, IAsyncEnumerable<ProductEntity>> GetOrderDetailsSummaryQuery =
		EF.CompileAsyncQuery((DatabaseContextGroceryStore context, Guid orderId) =>
			from p in context.Products.AsNoTracking()
			join o in context.OrderDetails on p.ProductId equals o.ProductId
			where o.OrderId == orderId
			select new ProductEntity(p.ProductId, p.Name, p.Description, p.Price,
				   new List<OrderDetailEntity>() { new(orderId, p.ProductId, p.Price, o.Quantity, o.Total) }));

	/// <summary>
	/// Obtiene los detalles de una orden
	/// </summary>
	/// <param name="orderId"></param>
	/// <returns>OrderDetailEntity</returns>
	public IAsyncEnumerable<ProductEntity> GetOrderDetailsSummaryByOrderId(Guid orderId)
		=> GetOrderDetailsSummaryQuery(this, orderId);
	#endregion Order Get Queries
	#endregion Normal User Methods

	#region Entities
	/// <summary>
	/// Entidad de productos
	/// </summary>
	public DbSet<ProductEntity> Products
	{ get; set; }

	/// <summary>
	/// Entidad de stock/inventario
	/// </summary>
	public DbSet<StockEntity> Stocks
	{ get; set; }

	/// <summary>
	/// Entidad de ordenes
	/// </summary>
	public DbSet<OrderEntity> Orders
	{ get; set; }

	/// <summary>
	/// Entidad de detalles de la orden
	/// </summary>
	public DbSet<OrderDetailEntity> OrderDetails
	{ get; set; }

	/// <summary>
	/// Entidad del usuario
	/// </summary>
	public DbSet<UserEntity> Users
	{ get; set; }
	#endregion Entities
}
