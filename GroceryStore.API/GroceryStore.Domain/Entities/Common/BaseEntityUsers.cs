namespace GroceryStore.Domain.Entities.Common;

using Microsoft.AspNetCore.Identity;

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
	public DateTime CreationDate 
	{ get; private set; }

	/// <summary>
	/// Fecha de modificacion del registro
	/// </summary>
	public DateTime? UpdateDate 
	{ get; private set; }
}