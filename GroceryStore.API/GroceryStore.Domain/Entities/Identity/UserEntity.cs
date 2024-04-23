using GroceryStore.Domain.Entities.Common;

namespace GroceryStore.Domain.Entities.Identity;

/// <summary>
/// Entidad que representa la tabla de usuarios (identity)
/// </summary>
public class UserEntity : BaseEntityUsers
{
	#pragma warning disable
	public UserEntity()
	{

	}
	#pragma warning enable

	public UserEntity(string names, string contact)
	{
		Names = names;
		Contact = contact;
	}

	public string Names
	{ get; private set; }

	public string Contact
	{ get; private set; }
}