using AutoMapper;
using GroceryStore.Common.Models.Grocery.Order;
using GroceryStore.Domain.Entities.Grocery.Order;

namespace GroceryStore.Common.Profiler.Grocery.OrderProfiler;

/// <summary>
/// Se encarga de mapear el modelo con su entidad
/// </summary>
public class OrderProfiler : Profile
{
    public OrderProfiler()
    {
        CreateMap<OrderModel, OrderEntity>().ReverseMap();
    }
}
