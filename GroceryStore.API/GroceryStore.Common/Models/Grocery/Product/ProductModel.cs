using GroceryStore.Common.Models.Grocery.Stock;
using System.ComponentModel;
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

    /// <summary>
    /// Nombre
    /// </summary>
    [Required(ErrorMessage = "The field {0} is required")]
    [MaxLength(250, ErrorMessage = "The field {0} must be equal or less than {1} characters")]
    public string Name
    { get; set; }

	/// <summary>
	/// Descripcion
	/// </summary>
	[MaxLength(250, ErrorMessage = "The field {0} must be equal or less than {1} characters")]
	public string? Description
    { get; set; }

    /// <summary>
    /// Precio
    /// </summary>
    [Required(ErrorMessage = "The field {0} is required")]
    [DefaultValue(10)]
    [Range(1, double.MaxValue, ErrorMessage = "The field {0} must be greater or equal to {1}")]
	public decimal Price
    { get; set; }

    /// <summary>
    /// Stock / inventario
    /// </summary>
    public StockModel Stock
    { get; set; }
}
