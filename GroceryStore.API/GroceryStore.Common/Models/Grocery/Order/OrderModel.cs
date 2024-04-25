using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Grocery.Order;

/// <summary>
/// Modelo que representa una orden
/// </summary>
public class OrderModel
{
	#pragma warning disable
	public OrderModel()
    {
        
    }
	#pragma warning enable

	public OrderModel(Guid userId, ICollection<OrderDetailModel> details)
	{
		UserId = userId;
		Details = details;
	}

	/// <summary>
	/// Id del usuario
	/// </summary>
	[JsonIgnore]
    public Guid UserId { get; set; }

	/// <summary>
	/// Productos a ordenar
	/// </summary>
	ICollection<OrderDetailModel> Details
	{ get; set; } = new List<OrderDetailModel>();
}
