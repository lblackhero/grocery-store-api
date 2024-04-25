using GroceryStore.Domain.Entities.Common;
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
    { get; private set; }

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
    public int OrderNumber 
    { get; private set; }

    /// <summary>
    /// Detalle de la orden (tabla dependiente)
    /// </summary>
    public ICollection<OrderDetailEntity> Details
    { get; private set; } = new List<OrderDetailEntity>();
}
