using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Grocery.Stock;

/// <summary>
/// Modelo de la entidad stock (admite valores nulos)
/// </summary>
public class StockModelToUpdate
{
    #pragma warning disable
    public StockModelToUpdate()
    {

    }
    #pragma warning enable

    public StockModelToUpdate(Guid productId, int? quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public int? Quantity
    { get; set; }

    [JsonIgnore]
    public Guid ProductId
    { get; set; }

    [JsonIgnore]
    public bool IsAvailable
    { get; set; }
}