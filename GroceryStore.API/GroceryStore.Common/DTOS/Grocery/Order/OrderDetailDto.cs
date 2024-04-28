namespace GroceryStore.Common.DTOS.Grocery.Order;

/// <summary>
/// DTO para el detalle de una orden
/// </summary>
/// <param name="productName">Nombre del producto</param>
/// <param name="productDescription">Descripcion del producto</param>
/// <param name="productPrice">Precio del producto</param>
/// <param name="productQuantity">Cantidad del producto ordenada</param>
/// <param name="productTotal">Total a pagar por producto</param>
public class OrderDetailDto(string productName, string? productDescription, decimal productPrice, int productQuantity, decimal productTotal)
{
    public string ProductName
    { get; set; } = productName;

    public string? ProductDescription
    { get; set; } = productDescription;

    public decimal ProductPrice
    { get; set; } = productPrice;

    public int ProductQuantity
    { get; set; } = productQuantity;

    public decimal ProductTotal
    { get; set; } = productTotal;
}
