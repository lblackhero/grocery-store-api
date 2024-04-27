using GroceryStore.Common.Models.Grocery.Stock;
using GroceryStore.Common.Models.Common.GlobalResponse;

namespace GroceryStore.Application.Interfaces.Grocery.Stock;

/// <summary>
/// Define los metodos para el manejo del stock
/// </summary>
public interface IStockRepository
{
	#region Put Methods
	/// <summary>
	/// Se encarga de actualizar varios stocks
	/// </summary>
	/// <param name="stockModels">Datos del stock</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> UpdateStocks(IEnumerable<StockModel> stockModels);
	#endregion Put Methods
}
