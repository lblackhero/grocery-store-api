using AutoMapper;
using GroceryStore.Common.Models.Grocery.Stock;
using GroceryStore.Domain.Entities.Grocery.Stock;

namespace GroceryStore.Common.Profiler.Grocery.StockProfiler;

/// <summary>
/// Se encarga de mapear el modelo con su entidad
/// </summary>
public class StockProfiler : Profile
{
    public StockProfiler()
    {
        CreateMap<StockModel, StockEntity>().ReverseMap();
    }
}