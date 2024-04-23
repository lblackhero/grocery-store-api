using System.Net;

namespace GroceryStore.Common.Models;

/// <summary>
/// Clase para manejar las respuestas de los metodos
/// </summary>
/// <param name="StatusCode">Codigo de respuesta de la solicitud</param>
/// <param name="ResponseData">Datos de respuesta de la solicitud</param>
/// <param name="ResponseMessage">Mensaje de respuesta de la solicitud</param>
public class ReturnResponses(HttpStatusCode StatusCode, object? ResponseData = null, string? ResponseMessage = null)
{
	/// <summary>
	/// Datos de respuesta
	/// </summary>
	public object? ResponseData
	{ get; private set; } = ResponseData;

	/// <summary>
	/// Mensaje de respuesta
	/// </summary>
	public string? ResponseMessage
	{ get; private set; } = ResponseMessage;

	/// <summary>
	/// Codigo HTTP de respuesta
	/// </summary>
	public HttpStatusCode StatusCode
	{ get; private set; } = StatusCode;
}