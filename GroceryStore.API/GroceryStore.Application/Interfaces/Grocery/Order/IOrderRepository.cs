using GroceryStore.Common.Models;

namespace GroceryStore.Application.Interfaces.Grocery.Order;

/// <summary>
/// Define los metodos para el manejo de ordenes
/// </summary>
public interface IOrderRepository
{
	#region Get Methods
	/// <summary>
	/// Se encarga de obtener los productos disponibles (con stock)
	/// </summary>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> GetAvailableProducts();
	#endregion Get Methods
}
