using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace GroceryStore.Presentation;

/// <summary>
/// Clase para manejar globalmente las excepciones
/// </summary>
/// <param name="next">Delegado</param>
/// <param name="accessor">Accesor</param>
public class ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor accessor)
{
    private readonly RequestDelegate next = next;
    private readonly IHttpContextAccessor accessor = accessor;

	/// <summary>
	/// Invoca el casteo de excepciones
	/// </summary>
	/// <param name="context">Context</param>
	/// <returns>Task</returns>
	public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            if (accessor.HttpContext != null)
                await HandleExceptionMessageAsync(accessor.HttpContext, ex).ConfigureAwait(false);
        }
    }

	/// <summary>
	/// Setea el mensaje de la excepcion
	/// </summary>
	/// <param name="context">Context</param>
	/// <param name="ex">Excepcion</param>
	/// <returns>Task</returns>
	private static Task HandleExceptionMessageAsync(HttpContext context, Exception ex)
    {
        var validationProblems = new ValidationProblemDetails()
        {
            Title = string.Concat("Ha ocurrido un error para el metodo ", context != null ? context.Request.Path : null),
            Status = (int)HttpStatusCode.InternalServerError,
            Instance = context?.Request.QueryString.Value,
            Detail = string.Concat("Error Message*", ex.Message, ex.InnerException != null ? string.Concat("Inner Exception*", ex.InnerException) : null, "Stack trace*", ex.StackTrace),
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(JsonSerializer.Serialize(validationProblems));
    }
}
