using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Grocery.Order;

/// <summary>
/// Modelo que representa el detalle de una orden o
/// la cantidad de productos a ordenar
/// </summary>
public class OrderDetailModel
{
    #pragma warning disable
	public OrderDetailModel()
    {
        
    }
	#pragma warning enable

	public OrderDetailModel(Guid orderId, Guid productId, decimal unitPrice, int quantity, decimal total)
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
	[JsonIgnore]
    public Guid OrderId 
    { get; set; }

	/// <summary>
	/// Id del producto
	/// </summary>
    [Required]
    public Guid ProductId 
    { get; set; }

	/// <summary>
	/// Precio por unidad segun el producto
	/// </summary>
	[JsonIgnore]
	public decimal UnitPrice
	{ get; set; }

	/// <summary>
	/// Valor total por producto
	/// </summary>
	[Required]
    public int Quantity 
    { get; set; }

	/// <summary>
	/// Total a pagar por producto
	/// </summary>
    [JsonIgnore]
    public decimal Total 
    { get; set; }
}
