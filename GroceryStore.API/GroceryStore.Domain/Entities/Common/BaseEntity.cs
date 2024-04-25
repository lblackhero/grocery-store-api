namespace GroceryStore.Domain.Entities.Common;

using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Entidad para tener un control sobre campos de fecha al modificar/insertar registros
/// </summary>
public class BaseEntity
{
    public BaseEntity()
    {
        
    }

	public BaseEntity(DateTime creationDate, DateTime? updateDate)
	{
		CreationDate = creationDate;
		UpdateDate = updateDate;
	}

	/// <summary>
	/// Fecha de recacion del registro
	/// </summary>
	[Column("CreationDate")]
	public DateTime CreationDate 
	{ get; private set; }

	/// <summary>
	/// Fecha de modificacion del registro
	/// </summary>
	[Column("ModifiedDate")]
    public DateTime? UpdateDate 
	{ get; private set; }

	/// <summary>
	/// Obtiene la hora del servidor 
	/// </summary>
	/// <returns></returns>
	private static DateTime GetTimeColombia()
		=> TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")).DateTime;
	
	/// <summary>
	/// Setea la hora de creacion de un registro
	/// </summary>
	public void SetCreationDate() => CreationDate = GetTimeColombia();

	/// <summary>
	/// Setea la hora de actualizacion de un registro
	/// </summary>
	public void SetUpdateDate() => UpdateDate = GetTimeColombia();
}
