using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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

	[DefaultValue(0)]
	[Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater or equal to {1}")]
	public int? Quantity
    { get; set; }

    [JsonIgnore]
    public Guid ProductId
    { get; set; }

    [JsonIgnore]
    public bool IsAvailable
    { get; set; }
}
