using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Grocery.Stock;

/// <summary>
/// Modelo de la entidad stock
/// </summary>
public class StockModel
{
    #pragma warning disable
    public StockModel()
    {

    }
    #pragma warning enable

    public StockModel(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    [Required(ErrorMessage = "The field {0} is required")]
    public int Quantity
    { get; set; }

    [JsonIgnore]
    public Guid ProductId
    { get; set; }

    [JsonIgnore]
    public bool IsAvailable
    { get; set; }
}