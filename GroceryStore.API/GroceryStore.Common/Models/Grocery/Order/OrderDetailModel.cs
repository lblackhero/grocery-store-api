using GroceryStore.Common.Models.Common.BaseEntity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Grocery.Order;

/// <summary>
/// Modelo de la entidad detalle de la orden
/// </summary>
public class OrderDetailModel : BaseModel
{
    #pragma warning disable
	public OrderDetailModel()
    {
        
    }
	#pragma warning enable

	public OrderDetailModel(Guid productId, decimal unitPrice, int quantity, decimal total)
	{
		ProductId = productId;
		UnitPrice = unitPrice;
		Quantity = quantity;
		Total = total;
	}

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
	/// Cantidad solicitada del producto
	/// </summary>
	[Required]
	[DefaultValue(1)]
	[Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater or equal to {1}")]
	public int Quantity 
    { get; set; }

	/// <summary>
	/// Total a pagar por producto
	/// </summary>
    [JsonIgnore]
    public decimal Total 
    { get; set; }
}
