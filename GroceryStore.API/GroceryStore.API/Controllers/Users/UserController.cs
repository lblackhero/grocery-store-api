using GroceryStore.Application.Interfaces.User;
using GroceryStore.Common.Models;
using GroceryStore.Common.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStore.Presentation.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	#region Post Methods
	/// <summary>
	/// Se encarga de manejar el registro de un usuario
	/// </summary>
	/// <param name="registrationUserModel">Modelo de los datos del usuario</param>
	/// <param name="userRepository">Servicio de manejo de usuarios</param>
	/// <returns>ReturnResponses</returns>
	[HttpPost("register")]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ReturnResponses>> Register([FromBody]RegistrationUserModel registrationUserModel, [FromServices] IUserRepository userRepository)
	{
		ArgumentNullException.ThrowIfNull(registrationUserModel);
		var result = await userRepository.CreateUser(registrationUserModel);

		if (result.StatusCode != HttpStatusCode.OK)
			return BadRequest(result);

		return Ok(result);
	}

	/// <summary>
	/// Se encarga de cerrar la sesion del usuario
	/// </summary>
	/// <param name="userRepository">Servicio</param>
	/// <returns>ReturnResponses</returns>
	[Authorize]
	[HttpPost("logout")]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ReturnResponses>> LogOut([FromServices] IUserRepository userRepository) => 
		Ok(await userRepository.LogOut());
	#endregion Post Methods

	#region Get Methods
	/// <summary>
	/// Se encarga de manejar el login de un usuario
	/// </summary>
	/// <param name="loginUserModel">Modelo de los datos del usuario</param>
	/// <param name="userRepository">Servicio de manejo de usuarios</param>
	/// <returns>ReturnResponses</returns>
	[HttpGet("login")]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ReturnResponses>> Login([FromQuery]LoginUserModel loginUserModel, [FromServices] IUserRepository userRepository)
	{
		ArgumentNullException.ThrowIfNull(loginUserModel);
		var result = await userRepository.LogIn(loginUserModel);

		if (result.StatusCode != HttpStatusCode.OK) 
			return BadRequest(result);

		return Ok(result);
	}
	#endregion Get Methods
}
