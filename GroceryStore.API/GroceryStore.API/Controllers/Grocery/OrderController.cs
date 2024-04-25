using GroceryStore.Application.Interfaces.Grocery.Order;
using GroceryStore.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStore.Presentation.Controllers.Grocery;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
	#region Get Methods
	/// <summary>
	/// Se encarga de obtener las ordenes disponibles para usuarios registrados
	/// </summary>
	/// <param name="orderRepository">Servicio</param>
	/// <returns>ReturnResponses</returns>
	[Authorize]
	[HttpGet]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ReturnResponses>> GetAvailableProducts([FromServices] IOrderRepository orderRepository)
	{
		ReturnResponses availableProducts = await orderRepository.GetAvailableProducts();

		if (availableProducts.StatusCode != HttpStatusCode.OK)
			return NotFound(availableProducts);

		return Ok(availableProducts);
	}
	#endregion Get Methods
}
