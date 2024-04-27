using GroceryStore.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore.Domain.Entities.Grocery.Order;

/// <summary>
/// Entidad que representa la tabla de ordenes
/// </summary>
[Table("Order", Schema = "dbo")]
public class OrderEntity : BaseEntity
{
    #pragma warning disable
	public OrderEntity()
    {
        
    }
    #pragma warning enable

	public OrderEntity(Guid orderId, Guid userId, int orderNumber)
	{
		OrderId = orderId;
		UserId = userId;
		OrderNumber = orderNumber;
	}

    /// <summary>
    /// Id del producto
    /// </summary>
	[Key]
    [Column("OrderId")]
    public Guid OrderId 
    { get; private set; } = new Guid();

    /// <summary>
    /// Id del usuario
    /// </summary>
    [Column("UserId")]
    public Guid UserId 
    { get; private set; }

    /// <summary>
    /// Numero de orden
    /// </summary>
    [Column("OrderNumber")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderNumber 
    { get; private set; }

    /// <summary>
    /// Representa la sumatoria del valor de todos los productos ordenados
    /// </summary>
    [Column("TotalToPay")]
    [Precision(18, 2)]
    public decimal TotalToPay 
    { get; private set; }

    /// <summary>
    /// Detalle de la orden (tabla dependiente) - propiedad de navegacion
    /// </summary>
    public ICollection<OrderDetailEntity> Details
    { get; } = new List<OrderDetailEntity>();
}
