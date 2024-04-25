using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Common.Models.User;

/// <summary>
/// Modelo para manejar el registro de usuarios
/// </summary>
public class RegistrationUserModel
{
	public RegistrationUserModel(string fullName, string? contact, string email, string password, string confirmPassword, bool isAdmin)
	{
		FullName = fullName;
		Contact = contact;
		Email = email;
		Password = password;
		ConfirmPassword = confirmPassword;
		IsAdmin = isAdmin;
	}

	/// <summary>
	/// Nombre del usuario
	/// </summary>
	[Required]
    public string FullName 
	{ get; set; }

	/// <summary>
	/// Contacto
	/// </summary>
    public string? Contact 
	{ get; set; }

	/// <summary>
	/// Email (sera usado como id/username)
	/// </summary>
    [Required]
	[DataType(DataType.EmailAddress)]
	[EmailAddress]
	public string Email 
	{ get; set; }

	/// <summary>
	/// Contraseña
	/// </summary>
    [Required]
	[DataType(DataType.Password)]
	public string Password 
	{ get; set; }

	/// <summary>
	/// Confirmacion de la contraseña
	/// </summary>
	[Required]
	[DataType(DataType.Password)]
	[Compare(nameof(Password), ErrorMessage = "The password fields don't match")]
	public string ConfirmPassword 
	{ get; set; }

	/// <summary>
	/// Indica si el usuario es administrador o no
	/// </summary>
	[Required]
    public bool IsAdmin 
	{ get; set; }
}
