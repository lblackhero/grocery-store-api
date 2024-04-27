namespace GroceryStore.Domain.Entities.Common;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Entidad para tener un control sobre campos de fecha al insertar/modificar un registro (para identity)
/// </summary>
public class BaseEntityUsers : IdentityUser
{
    public BaseEntityUsers()
    {
        
    }

	public BaseEntityUsers(DateTime creationDate, DateTime? updateDate)
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
	[Column("UpdateDate")]
	public DateTime? UpdateDate 
	{ get; private set; }

	/// <summary>
	/// Obtiene la hora del servidor 
	/// </summary>
	/// <returns>DateTime</returns>
	private static DateTime GetTimeColombia()
		=> TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")).DateTime;

	/// <summary>
	/// Setea la hora de creacion de un registro
	/// </summary>
	public void SetCreationDate() => CreationDate = GetTimeColombia();
}
