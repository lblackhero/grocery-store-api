using GroceryStore.Common.Models.Grocery.Stock;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Common.Models.Grocery.Product;

/// <summary>
/// Modelo de la entidad producto
/// </summary>
public class ProductModel
{
    #pragma warning disable
    public ProductModel()
    {

    }
    #pragma warning enable

    public ProductModel(string name, string? description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    [Required(ErrorMessage = "The field {0} is required")]
    public string Name
    { get; set; }

    public string? Description
    { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public decimal Price
    { get; set; }

    public StockModel Stock
    { get; set; }
}