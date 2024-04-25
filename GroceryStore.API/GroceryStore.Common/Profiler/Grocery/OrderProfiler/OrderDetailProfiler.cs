using AutoMapper;
using GroceryStore.Common.Models.Grocery.Order;
using GroceryStore.Domain.Entities.Grocery.Order;

namespace GroceryStore.Common.Profiler.Grocery.OrderProfiler;

/// <summary>
/// Se encarga de mapear el modelo con su entidad
/// </summary>
public class OrderDetailProfiler : Profile
{
    public OrderDetailProfiler()
    {
        CreateMap<OrderDetailModel, OrderDetailEntity>().ReverseMap();
    }
}
