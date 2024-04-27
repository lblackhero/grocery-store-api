using GroceryStore.Common.Models.Common.GlobalResponse;
using GroceryStore.Common.Models.Grocery.Order;

namespace GroceryStore.Application.Interfaces.Grocery.Order;

/// <summary>
/// Define los metodos para el manejo de ordenes
/// </summary>
public interface IOrderRepository
{
	#region Post Methods
	/// <summary>
	/// Se encarga de gestionar todos los procesos relacionados
	/// que se ejecutan al realizar una orden
	/// </summary>
	/// <param name="userOrder">Orden del usuario</param>
	/// <param name="userNameIdentifier">Id del usuario</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> BuyProducts(List<OrderDetailModel> userOrder, string userNameIdentifier);

	/// <summary>
	/// Se encarga de guardar los datos de una orden
	/// </summary>
	/// <param name="order">Datos de la orden</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> CreateOrderAsync(OrderModel order);
	#endregion Post Methods
}
