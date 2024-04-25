using GroceryStore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore.Domain.Entities.Identity;

/// <summary>
/// Entidad que representa la tabla de usuarios (identity)
/// </summary>
[Table("AspNetUsers", Schema = "dbo")]
public class UserEntity : BaseEntityUsers
{
	#pragma warning disable
	public UserEntity()
	{

	}
	#pragma warning enable

	public UserEntity(string fullName, string? contact)
	{
		FullName = fullName;
		Contact = contact;
	}

	/// <summary>
	/// Nombre del usuario
	/// </summary>
	[Column("Names")]
	public string FullName
	{ get; set; }

	/// <summary>
	/// Contacto del usuario
	/// </summary>
	[Column("Contact")]
	public string? Contact
	{ get; set; }
}
