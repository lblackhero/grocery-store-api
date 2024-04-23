using AutoMapper;
using GroceryStore.Common.Models.Grocery.Product;
using GroceryStore.Domain.Entities.Grocery.Products;

namespace GroceryStore.Common.Profiler.Grocery.ProductProfiler;

/// <summary>
/// Se encarga de mapear el modelo con su entidad
/// </summary>
public class ProductProfiler : Profile
{
    public ProductProfiler()
    {
        CreateMap<ProductModel, ProductEntity>().ReverseMap();
    }
}