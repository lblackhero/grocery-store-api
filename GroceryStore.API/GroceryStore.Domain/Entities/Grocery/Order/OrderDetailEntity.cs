using GroceryStore.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GroceryStore.Domain.Entities.Grocery.Order;

/// <summary>
/// Entidad que representa la tabla de detalle de ordenes
/// </summary>
[Table("OrderDetail", Schema = "dbo")]
public class OrderDetailEntity : BaseEntity
{
	#pragma warning disable
	public OrderDetailEntity()
    {
        
    }
	#pragma warning enable

	public OrderDetailEntity(Guid orderId, Guid productId, decimal unitPrice, int quantity, decimal total)
	{
		OrderId = orderId;
		ProductId = productId;
		UnitPrice = unitPrice;
		Quantity = quantity;
		Total = total;
	}

	/// <summary>
	/// Id de la orden
	/// </summary>
	[Column("OrderId")]
	public Guid OrderId
	{ get; private set; } = new Guid();

	/// <summary>
	/// Id del producto
	/// </summary>
	[Column("ProductId")]
    public Guid ProductId 
	{ get; private set; }

	/// <summary>
	/// Precio por unidad del producto
	/// </summary>
	[Column("UnitPrice")]
	[Precision(18, 2)]
	public decimal UnitPrice 
	{ get; private set; }

	/// <summary>
	/// Cantidad del producto
	/// </summary>
	[Column("Quantity")]
    public int Quantity 
	{ get; private set; }

	/// <summary>
	/// Valor total por producto
	/// </summary>
	[Column("Total")]
	[Precision(18, 2)]
	public decimal Total 
	{ get; private set; }

	/// <summary>
	/// Propiedad de navegacion a la tabla padre (no mapeada en el json)
	/// </summary>
	[JsonIgnore]
	public OrderEntity Order
	{ get; private set; } = null!;
}
