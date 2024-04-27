using GroceryStore.Common.Models.Common.BaseEntity;
using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Grocery.Order;

/// <summary>
/// Modelo que representa la entidad orden
/// </summary>
public class OrderModel : BaseModel
{
	#pragma warning disable
	public OrderModel()
    {
        
    }
	#pragma warning enable

	public OrderModel(Guid userId, decimal totalToPay, ICollection<OrderDetailModel> details)
	{
		UserId = userId;
		TotalToPay = totalToPay;
		Details = details;
	}

	/// <summary>
	/// Id del usuario
	/// </summary>
	[JsonIgnore]
    public Guid UserId { get; set; }

	/// <summary>
	/// Representa la sumatoria del valor de todos los productos ordenados
	/// </summary>
	[JsonIgnore]
	public decimal TotalToPay
	{ get; set; }

	/// <summary>
	/// Productos a ordenar
	/// </summary>
	public ICollection<OrderDetailModel> Details
	{ get; set; } = new List<OrderDetailModel>();
}
