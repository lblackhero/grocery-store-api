using GroceryStore.Application.Interfaces.Email;
using GroceryStore.Application.Interfaces.Grocery.Order;
using GroceryStore.Application.Interfaces.Grocery.Products;
using GroceryStore.Common.DTOS.Email;
using GroceryStore.Common.DTOS.Grocery.Order;
using GroceryStore.Common.Models.Common.GlobalResponse;
using GroceryStore.Common.Models.Grocery.Order;
using GroceryStore.Common.Statics.Common;
using GroceryStore.Common.Statics.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace GroceryStore.Presentation.Controllers.Grocery;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
	static readonly int MaxOrderLength = 100;

	#region Get Methods
	/// <summary>
	/// Obtiene todos los productos disponibles para ordenar
	/// </summary>
	/// <param name="productRepository">Servicio</param>
	/// <returns>ActionResult</returns>
	[Authorize(Roles = UserStatics.User)]
	[HttpGet]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ReturnResponses>> GetAvailableProducts([FromServices] IProductRepository productRepository)
	{
		ReturnResponses availableProducts = await productRepository.GetAvailableProducts();

		if (availableProducts.StatusCode != HttpStatusCode.OK)
			return NotFound(availableProducts);

		return Ok(availableProducts);
	}
	#endregion Get Methods

	#region Post Methods
	/// <summary>
	/// Se encarga de gestionar el proceso de compra de uno o mas productos
	/// </summary>
	/// <param name="userOrder">Orden del usuario</param>
	/// <param name="orderRepository">Servicio</param>
	/// <returns>ActionResult</returns>
	[Authorize(Roles = UserStatics.User)]
	[HttpPost]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ReturnResponses>> BuyProducts([FromBody] List<OrderDetailModel> userOrder, [FromServices] IOrderRepository orderRepository, [FromServices] IEmailRepository emailRepository)
	{
		Claim? userClaimNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

		ArgumentNullException.ThrowIfNull(userOrder);
		ArgumentNullException.ThrowIfNull(User.Identity);
		ArgumentNullException.ThrowIfNull(userClaimNameIdentifier);
		ArgumentOutOfRangeException.ThrowIfEqual(MaxOrderLength, userOrder.Count);

		ReturnResponses processingOrderTask = await orderRepository.BuyProducts(userOrder, userClaimNameIdentifier.Value);

		if (processingOrderTask.StatusCode != HttpStatusCode.OK)
			return BadRequest(processingOrderTask);

		Guid orderId = Guid.TryParse(processingOrderTask.ResponseData?.ToString(), out Guid result) ? result : Guid.Empty;
		OrderSummaryDto orderSummary = await orderRepository.GetOrderSummary(orderId, userClaimNameIdentifier.Value);

		EmailDto email = new(orderSummary.Email ?? string.Empty, $"Gracias por tu pedido {orderSummary.FullName}", "OrderConfirmation", orderSummary);
		ReturnResponses emailSendTask = await emailRepository.ManageEmailSend(email);

		if (emailSendTask.StatusCode != HttpStatusCode.OK) 
			return BadRequest(emailSendTask);

		return Ok(new ReturnResponses(HttpStatusCode.OK, orderSummary, GenericResponse.GenericOkMessage));
	}
	#endregion Post Methods
}
