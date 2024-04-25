using GroceryStore.Common.Models.Grocery.Stock;
using System.ComponentModel;
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

    /// <summary>
    /// Id
    /// </summary>
    [Required]
    public Guid ProductId
    { get; set; }

	/// <summary>
	/// Descripcion
	/// </summary>
	[MaxLength(250, ErrorMessage = "The field {0} must be equal or less than {1} characters")]
	public string? Description
    { get; set; }

	/// <summary>
	/// Nombre
	/// </summary>
	[MaxLength(250, ErrorMessage = "The field {0} must be equal or less than {1} characters")]
	public string? Name
    { get; set; }

	/// <summary>
	/// Precio
	/// </summary>
	[DefaultValue(10)]
	[Range(1, double.MaxValue, ErrorMessage = "The field {0} must be greater or equal to {1}")]
	public decimal? Price
    { get; set; }

    /// <summary>
    /// Stock / inventario
    /// </summary>
    public StockModelToUpdate? Stock
    { get; set; }
}
