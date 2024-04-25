using System.ComponentModel;
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

    /// <summary>
    /// Cantidad del producto
    /// </summary>
    [Required(ErrorMessage = "The field {0} is required")]
    [DefaultValue(1)]
    [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater or equal to {1}")]
    public int Quantity
    { get; set; }

    /// <summary>
    /// Id del producto
    /// </summary>
    [JsonIgnore]
    public Guid ProductId
    { get; set; }

    /// <summary>
    /// Esta el producto disponible
    /// </summary>
    [JsonIgnore]
    public bool IsAvailable
    { get; set; }
}
