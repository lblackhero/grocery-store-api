using GroceryStore.Domain.Entities.Common;
using GroceryStore.Domain.Entities.Grocery.Stock;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore.Domain.Entities.Grocery.Products;

/// <summary>
/// Entidad que representa la tabla producto
/// </summary>
[Table("Product", Schema = "dbo")]
public class ProductEntity : BaseEntity
{
	#pragma warning disable
	public ProductEntity()
    {
        
    }
	#pragma warning enable

	public ProductEntity(Guid productId, string name, string? description, decimal price)
	{
		ProductId = productId;
		Name = name;
		Description = description;
		Price = price;
	}

	/// <summary>
	/// Id del producto
	/// </summary>
	[Key]
	[Column("ProductId")]
	public Guid ProductId 
    { get; set; }

	/// <summary>
	/// Nombre del producto
	/// </summary>
	[Column("Name")]
	public string Name 
    { get; private set; }

	/// <summary>
	/// Descripcion del producto
	/// </summary>
	[Column("Description")]
    public string? Description
    { get; private set; }

	/// <summary>
	/// Precio del producto
	/// </summary>
	[Column("Price")]
    public decimal Price
    { get; private set; }

	/// <summary>
	/// Stock del producto (tabla dependiente)
	/// </summary>
	public StockEntity Stock 
	{ get; private set; }

	/// <summary>
	/// Actualiza los campos del producto
	/// </summary>
	/// <param name="name">Nombre del producto</param>
	/// <param name="description">Descripcion del producto</param>
	/// <param name="price">Precio del producto</param>
	public void UpdateProductFields(string name, decimal price, string? description = null)
	{
		Name = name;
		Description = description;
		Price = price;
	}
}