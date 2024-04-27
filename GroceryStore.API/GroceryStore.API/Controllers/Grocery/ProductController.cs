using GroceryStore.Application.Interfaces.Grocery.Products;
using GroceryStore.Common.Models.Common.GlobalResponse;
using GroceryStore.Common.Models.Grocery.Product;
using GroceryStore.Common.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStore.Presentation.Controllers.Grocery;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	#region Get Methods
	/// <summary>
	/// Obtiene un producto y su stock por id
	/// </summary>
	/// <param name="productId">Id del producto</param>
	/// <param name="productRepository">Servicio</param>
	/// <returns>ReturnResponses</returns>
	[Authorize(Roles = UserStatics.Admin)]
    [HttpGet("/api/[controller]/{productId:guid}")]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReturnResponses>> GetProductById(Guid productId, [FromServices] IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ReturnResponses product = await productRepository.GetProductAndStockByProductIdAsync(productId);

        if (product.StatusCode != HttpStatusCode.OK)
            return NotFound(product);

        return Ok(product);
    }

    /// <summary>
    /// Obtiene todos los productos con su stock
    /// </summary>
    /// <param name="productRepository">Servicio</param>
    /// <returns>ReturnResponses</returns>
    [Authorize(Roles = UserStatics.Admin)]
    [HttpGet]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReturnResponses>> GetProducts([FromServices] IProductRepository productRepository)
    {
        ReturnResponses product = await productRepository.GetProductsAndStockAsync();

        if (product.StatusCode != HttpStatusCode.OK)
            return NotFound(product);

        return Ok(product);
    }
	#endregion Get Methods

	#region Post Methods
	/// <summary>
	/// Crea un nuevo producto
	/// </summary>
	/// <param name="product">Datos del producto</param>
	/// <param name="productsRepository">Servicio</param>
	/// <returns>ReturnResponses</returns>
	[Authorize(Roles = UserStatics.Admin)]
	[HttpPost]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReturnResponses>> AddProduct([FromBody] ProductModel product, [FromServices] IProductRepository productsRepository)
    {
        ArgumentNullException.ThrowIfNull(product);
        ReturnResponses result = await productsRepository.AddProductAsync(product);

        if (result.StatusCode != HttpStatusCode.Created)
            return BadRequest(result);

        return Created(nameof(AddProduct), result);
    }
	#endregion Post Methods

	#region Put Methods
	/// <summary>
	/// Actualiza un producto
	/// </summary>
	/// <param name="product">Datos del producto</param>
	/// <param name="productsRepository">Servicio</param>
	/// <returns>ReturnResponses</returns>
	[Authorize(Roles = UserStatics.Admin)]
	[HttpPut]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReturnResponses>> UpdateProductByIdAsync([FromBody] ProductModelToUpdate product, [FromServices] IProductRepository productsRepository)
    {
        ArgumentNullException.ThrowIfNull(product);
        ReturnResponses productUpdateTask = await productsRepository.UpdateProductByIdAsync(product);

        if (productUpdateTask.StatusCode != HttpStatusCode.OK)
            return NotFound(productUpdateTask);

        return Ok(productUpdateTask);
    }
	#endregion Put Methods

	#region Delete Methods
	/// <summary>
	/// Se encarga de borrar un producto
	/// </summary>
	/// <param name="productId">Id del producto</param>
	/// <param name="productsRepository">Servicio</param>
	/// <returns>ReturnResponses</returns>
	[Authorize(Roles = UserStatics.Admin)]
	[HttpDelete("/api/[controller]/{productId:guid}")]
    [ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ReturnResponses), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReturnResponses>> DeleteProductByIdAsync(Guid productId, [FromServices] IProductRepository productsRepository)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ReturnResponses productDeleteTask = await productsRepository.DeleteProductByIdAsync(productId);

        if (productDeleteTask.StatusCode != HttpStatusCode.NoContent)
            return NotFound(productDeleteTask);

        return NoContent();
    }
    #endregion Delete Methods
}
