using GroceryStore.Application.Interfaces.User;
using GroceryStore.Common.Models;
using GroceryStore.Common.Models.User;
using GroceryStore.Common.Statics;
using GroceryStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace GroceryStore.Logic.Repositories.User;

public class UserRepository(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, SignInManager<UserEntity> signInManager) : IUserRepository
{
	private readonly UserManager<UserEntity> UserManager = userManager;
	private readonly RoleManager<IdentityRole> RoleManager = roleManager;
	private readonly SignInManager<UserEntity> SignInManager = signInManager;

	#region Post Methods
	/// <summary>
	/// Se encarga de crear un usuario
	/// </summary>
	/// <param name="registrationUserModel">Datos del usuario</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> CreateUser(RegistrationUserModel registrationUserModel)
	{
		UserEntity user = new()
		{
			FullName = registrationUserModel.FullName,
			Contact = registrationUserModel.Contact,
			Email = registrationUserModel.Email,
			UserName = registrationUserModel.Email,
			EmailConfirmed = true
		};

		user.SetCreationDate();
		IdentityResult userCreationTask = await UserManager.CreateAsync(user, registrationUserModel.Password);

		if (!userCreationTask.Succeeded)
			return new(HttpStatusCode.BadRequest, userCreationTask.Errors.Select(e => e.Description), "One or more errors occurred during the creation of the user");

		string roleToCreate = registrationUserModel.IsAdmin ? UserStatics.Admin : UserStatics.User;

		if (!await EnsureUserRolExists(roleToCreate))
			return new(HttpStatusCode.BadRequest, ResponseMessage: "An error ocurred during the creation of the user role");

		IdentityResult addRoleToUserTask  = await UserManager.AddToRoleAsync(user, roleToCreate);

		if (!addRoleToUserTask.Succeeded)
			return new(HttpStatusCode.BadRequest, addRoleToUserTask.Errors.Select(e => e.Description), "One or more errors occurred while assigning the role");

		return new(HttpStatusCode.OK, ResponseMessage: GenericResponse.GenericOkMessage);
	}

	/// <summary>
	/// Se encarga de cerrar la sesion del usuario
	/// </summary>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> LogOut()
	{
		await SignInManager.SignOutAsync();

		return new(HttpStatusCode.OK, ResponseMessage: GenericResponse.GenericOkMessage);
	}
	#endregion Post Methods

	#region Get Methods
	/// <summary>
	/// Se encarga de gestionar el acceso de un usuario registrado
	/// </summary>
	/// <param name="loginUserModel"></param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> LogIn(LoginUserModel loginUserModel)
	{
		var result = await SignInManager.PasswordSignInAsync(loginUserModel.UserName, loginUserModel.Password, false, false);

		if (!result.Succeeded)
			return new(HttpStatusCode.BadRequest, ResponseMessage: "An error occurred during the login process");

		return new(HttpStatusCode.OK, ResponseMessage: GenericResponse.GenericOkMessage);
	}
	#endregion Get Methods

	#region Utility Methods
	/// <summary>
	/// Valida si el rol a crear existe, si no lo crea
	/// </summary>
	/// <param name="roleToCreate">Nombre del rol</param>
	/// <returns>bool</returns>
	private async Task<bool> EnsureUserRolExists(string roleToCreate)
	{
		if (await RoleManager.RoleExistsAsync(roleToCreate))
			return true;

		IdentityResult roleCreation = await RoleManager.CreateAsync(new IdentityRole(roleToCreate));

		return roleCreation.Succeeded;
	}
	#endregion Utility Methods
}
