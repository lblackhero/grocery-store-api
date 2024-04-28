namespace GroceryStore.Common.DTOS.Grocery.Order;

/// <summary>
/// DTO para el resumen de una orden
/// </summary>
public class OrderSummaryDto
{
    #pragma warning disable
    public OrderSummaryDto()
    {

    }
    #pragma warning enable

    public OrderSummaryDto(string? email, string fullName, string? contact, int orderNumber, decimal totalToPay, DateTime orderCreationDate)
    {
        Email = email;
        FullName = fullName;
        Contact = contact;
        OrderNumber = orderNumber;
        TotalToPay = totalToPay;
        OrderCreationDate = orderCreationDate;
    }

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string? Email
    { get; set; }

    /// <summary>
    /// Nombre completo del usuario
    /// </summary>
    public string FullName
    { get; set; }

    /// <summary>
    /// Numero de contacto del usuario
    /// </summary>
    public string? Contact
    { get; set; }

    /// <summary>
    /// Numero de la orden
    /// </summary>
    public int OrderNumber
    { get; set; }

    /// <summary>
    /// Total a pagar
    /// </summary>
    public decimal TotalToPay
    { get; set; }

    /// <summary>
    /// Fecha de creacion de la orden
    /// </summary>
    public DateTime OrderCreationDate
    { get; set; }

    /// <summary>
    /// Detalle de la orden
    /// </summary>
    public List<OrderDetailDto> Details
    { get; set; } = [];
}
