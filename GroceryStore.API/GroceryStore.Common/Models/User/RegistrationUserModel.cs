using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Common.Models.User;

/// <summary>
/// Modelo para manejar el registro de usuarios
/// </summary>
public class RegistrationUserModel(string fullName, string? contact, string email, string password, string confirmPassword, bool isAdmin)
{
	/// <summary>
	/// Nombre del usuario
	/// </summary>
	[Required]
	public string FullName
	{ get; set; } = fullName;

	/// <summary>
	/// Contacto
	/// </summary>
	public string? Contact
	{ get; set; } = contact;

	/// <summary>
	/// Email (sera usado como id/username)
	/// </summary>
	[Required]
	[DataType(DataType.EmailAddress)]
	[EmailAddress]
	[MaxLength(256)]
	public string Email
	{ get; set; } = email;

	/// <summary>
	/// Contraseña
	/// </summary>
	[Required]
	[DataType(DataType.Password)]
	public string Password
	{ get; set; } = password;

	/// <summary>
	/// Confirmacion de la contraseña
	/// </summary>
	[Required]
	[DataType(DataType.Password)]
	[Compare(nameof(Password), ErrorMessage = "The password fields don't match")]
	public string ConfirmPassword
	{ get; set; } = confirmPassword;

	/// <summary>
	/// Indica si el usuario es administrador o no
	/// </summary>
	[Required]
	public bool IsAdmin
	{ get; set; } = isAdmin;
}
