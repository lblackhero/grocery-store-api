using GroceryStore.Common.Models.Grocery.Stock;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Common.Models.Grocery.Product;

/// <summary>
/// Modelo de la entidad producto (admite valores nulos)
/// </summary>
public class ProductModelToUpdate
{
    #pragma warning disable
    public ProductModelToUpdate()
    {

    }
    #pragma warning enable

    public ProductModelToUpdate(string? name, string? description, decimal? price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    [Required]
    public Guid ProductId
    { get; set; }

    public string? Description
    { get; set; }

    public string? Name
    { get; set; }

    public decimal? Price
    { get; set; }

    public StockModelToUpdate? Stock
    { get; set; }
}