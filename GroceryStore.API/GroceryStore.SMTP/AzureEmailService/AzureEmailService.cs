using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;

namespace GroceryStore.AzureCommunicationEmail.AzureEmailService;

public class AzureEmailService (IConfiguration configuration, IServiceProvider serviceProvider, IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider)
{
	private readonly IConfiguration Configuration = configuration;
	private readonly IServiceProvider ServiceProvider = serviceProvider;
	private readonly IRazorViewEngine RazorViewEngine = razorViewEngine;
	private readonly ITempDataProvider TempDataProvider = tempDataProvider;

	/// <summary>
	/// Se encarga de enviar un email 
	/// </summary>
	/// <param name="recipientAddress">Destinatario del mensaje</param>
	/// <param name="subject">Asunto del mensaje</param>
	/// <param name="htmlContent">Contenido HTML</param>
	/// <returns>bool</returns>
	public async Task<bool> SendEmail(string recipientAddress, string subject, string htmlContent)
	{
		var emailClient = new EmailClient(Configuration.GetConnectionString("AzureEmailServiceConnectionString"));

		EmailSendOperation emailSendOperation = emailClient.Send(
			WaitUntil.Completed,
			senderAddress: Configuration.GetSection("AzureEmailServiceConfiguration:EmailSender").Value,
			recipientAddress: recipientAddress,
			subject: subject,
			htmlContent: htmlContent);

		var result = await emailSendOperation.WaitForCompletionAsync();

		return result.Value.Status == EmailSendStatus.Succeeded;
	}

	#region Utility Methods
	/// <summary>
	/// Se encarga de convertir contenido html
	/// en string
	/// </summary>
	/// <param name="viewName">Nombre de la vista</param>
	/// <param name="model">Modelo de datos</param>
	/// <returns>string</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public async Task<string> RenderToStringAsync(string viewName, object model)
	{
		var httpContext = new DefaultHttpContext { RequestServices = ServiceProvider };
		var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

		using var sw = new StringWriter();
		var viewResult = RazorViewEngine.GetView(viewName, viewName, false);

		if (viewResult.View == null)
			throw new ArgumentNullException(string.Concat("La vista ", viewName, " no se ha encontrado"));

		var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
		{
			Model = model
		};

		var viewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext(
			actionContext,
			viewResult.View,
			viewDictionary,
			new TempDataDictionary(actionContext.HttpContext, TempDataProvider),
			sw,
			new HtmlHelperOptions()
		);

		await viewResult.View.RenderAsync(viewContext);
		return sw.ToString();
	}
	#endregion Utility Methods

}
