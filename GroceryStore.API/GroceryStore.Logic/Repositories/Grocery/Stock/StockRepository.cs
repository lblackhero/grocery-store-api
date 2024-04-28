using AutoMapper;
using GroceryStore.Application.Interfaces.Grocery.Stock;
using GroceryStore.Common.Functionalities;
using GroceryStore.Common.Models.Common.GlobalResponse;
using GroceryStore.Common.Models.Grocery.Stock;
using GroceryStore.Common.Statics.Common;
using GroceryStore.Domain.Entities.Grocery.Stock;
using GroceryStore.Infraestructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GroceryStore.Logic.Repositories.Grocery.Stock;

public class StockRepository(DatabaseContextGroceryStore databaseContextGroceryStore, IMapper mapper, IFunctionalities functionalities) : IStockRepository
{
	private readonly DatabaseContextGroceryStore DatabaseContextGroceryStore = databaseContextGroceryStore;
	private readonly IFunctionalities Functionalities = functionalities;
	private readonly IMapper Mapper = mapper;

	#region Put Methods
	/// <summary>
	/// Se encarga de actualizar varios stocks
	/// </summary>
	/// <param name="stockModels">Datos del stock</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> UpdateStocks(IEnumerable<StockModel> stockModels)
	{
		IEnumerable<StockEntity> stocks = Mapper.Map<IEnumerable<StockEntity>>(stockModels);

		try
		{
			DatabaseContextGroceryStore.Stocks.UpdateRange(stocks);
			await DatabaseContextGroceryStore.SaveChangesAsync(true);

			int result = await DatabaseContextGroceryStore.Stocks
				.Where(e => stocks.Contains(e))
				.ExecuteUpdateAsync(p => p.SetProperty(c => c.UpdateDate, Functionalities.GetTimeColombia()));

			if (result <= 0)
				return new(HttpStatusCode.BadRequest, ResponseMessage: "An error ocurred while trying to update the product(s) stock");
			
			return new(HttpStatusCode.OK, ResponseMessage: GenericResponse.GenericOkMessage);
		}
		catch
		{
			return new(HttpStatusCode.BadRequest, ResponseMessage: "An error ocurred while trying to update the product(s) stock");
		}
	}
	#endregion Put Methods

}
