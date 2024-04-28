using GroceryStore.Common.Models.Grocery.Order;
using GroceryStore.Common.Models.Grocery.Stock;

namespace GroceryStore.Common.DTOS.Grocery.Order;

/// <summary>
/// DTO que contiene datos relacionados a la orden
/// </summary>
/// <param name="userOrder">Datos con la orden del usuario</param>
/// <param name="availableProducts">Datos con los productos disponibles</param>
/// <param name="availableStock">Stock de los productos disponibles</param>
public class OrderProcessingDto(List<OrderDetailModel> userOrder, List<OrderDetailModel> availableProducts, List<StockModel> availableStock)
{
    /// <summary>
    /// Orden del usuario
    /// </summary>
    public List<OrderDetailModel> UserOrder
    { get; set; } = userOrder;

    /// <summary>
    /// Productos disponibles
    /// </summary>
    public List<OrderDetailModel> AvailableProducts
    { get; set; } = availableProducts;

    /// <summary>
    /// Stock de los productos disponibles
    /// </summary>
    public List<StockModel> AvailableStock
    { get; set; } = availableStock;
}
