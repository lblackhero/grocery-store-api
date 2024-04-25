namespace GroceryStore.Common.Models.Grocery.Order;

/// <summary>
/// Modelo que representa los productos disponibles
/// </summary>
public class AvailableProductModel
{
	#pragma warning disable
	public AvailableProductModel()
	{

	}
	#pragma warning enable

	public AvailableProductModel(Guid productId, string name, string? description, decimal price, int quantity)
	{
		ProductId = productId;
		Name = name;
		Description = description;
		Price = price;
		Quantity = quantity;
	}

	/// <summary>
	/// Id del producto
	/// </summary>
    public Guid ProductId 
	{ get; set; }

    /// <summary>
    /// Nombre del producto
    /// </summary>
    public string Name 
    { get; set; }

	/// <summary>
	/// Descripcion del producto
	/// </summary>
    public string? Description 
    { get; set; }

	/// <summary>
	/// Precio del producto
	/// </summary>
    public decimal Price 
    { get; set; }

	/// <summary>
	/// Cantidad del producto
	/// </summary>
    public int Quantity 
    { get; set; }
}
