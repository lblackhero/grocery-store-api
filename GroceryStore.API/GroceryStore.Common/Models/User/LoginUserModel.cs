using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Common.Models.User;

/// <summary>
/// Modelo para gestionar el login de usuarios
/// </summary>
public class LoginUserModel
{
	#pragma warning disable
	public LoginUserModel()
	{

	}
	#pragma warning enable

    public LoginUserModel(string userName, string password)
	{
		UserName = userName;
		Password = password;
	}

	/// <summary>
	/// Id del usuario (email)
	/// </summary>
	[Required]
	[DataType(DataType.EmailAddress)]
	[EmailAddress]
	public string UserName 
	{ get; set; }

	/// <summary>
	/// Contraseña
	/// </summary>
    [Required]
	[DataType(DataType.Password)]
	public string Password 
	{ get; set; }
}
