using GroceryStore.Common.Models.User;
using GroceryStore.Common.Models;

namespace GroceryStore.Application.Interfaces.User;

/// <summary>
/// Define los metodos para el manejo de los usuarios de la aplicacion
/// </summary>
public interface IUserRepository
{
	#region Post Methods
	/// <summary>
	/// Se encarga de crear un usuario
	/// </summary>
	/// <param name="registrationUserModel">Datos del usuario</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> CreateUser(RegistrationUserModel registrationUserModel);

	/// <summary>
	/// Se encarga de cerrar la sesion del usuario
	/// </summary>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> LogOut();
	#endregion Post Methods

	#region Get Methods
	/// <summary>
	/// Se encarga de gestionar el acceso de un usuario registrado
	/// </summary>
	/// <param name="loginUserModel"></param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> LogIn(LoginUserModel loginUserModel);
	#endregion Get Methods
}
