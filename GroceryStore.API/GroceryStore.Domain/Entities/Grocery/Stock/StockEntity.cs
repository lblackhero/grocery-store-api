using GroceryStore.Domain.Entities.Common;
using GroceryStore.Domain.Entities.Grocery.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GroceryStore.Domain.Entities.Grocery.Stock;

/// <summary>
/// Entidad que representa la tabla stock
/// </summary>
[Table("Stock", Schema = "dbo")]
public class StockEntity : BaseEntity
{
    #pragma warning disable
	public StockEntity()
    {
        
    }
    #pragma warning enable

	public StockEntity(Guid productId, int quantity, bool isAvailable)
	{
		ProductId = productId;
		Quantity = quantity;
		IsAvailable = isAvailable;
	}

    /// <summary>
    /// Id del producto
    /// </summary>
	[Key, ForeignKey("ProductId")]
	[Column("ProductId")]
    public Guid ProductId 
    { get; private set; }

    /// <summary>
    /// Cantidad de elementos del producto
    /// </summary>
    [Column("Quantity")]
    public int Quantity 
    { get; private set; }

    /// <summary>
    /// Esta disponible el producto
    /// </summary>
    [Column("IsAvailable")]
    public bool IsAvailable
	{ get; private set; }

    /// <summary>
    /// Propiedad de navegacion a la tabla padre (no mapeada en el json)
    /// </summary>
    [JsonIgnore]
    public ProductEntity Product 
    { get; private set; }

    /// <summary>
    /// Actualiza los campos del Stock
    /// </summary>
    /// <param name="quantity">Cantidad</param>
    /// <param name="isAvailable">Esta disponible</param>
	public void UpdateStockFields(int quantity, bool isAvailable)
	{
        Quantity = quantity;
        IsAvailable = isAvailable;
	}
}
