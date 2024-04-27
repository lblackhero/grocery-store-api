namespace GroceryStore.AzureCommunicationEmail.AzureEmailService;

/// <summary>
/// Define los metodos para el envio de correos usando Azure
/// </summary>
public interface IAzureEmailService
{
	/// <summary>
	/// Se encarga de enviar un email 
	/// </summary>
	/// <param name="recipientAddress">Destinatario del mensaje</param>
	/// <param name="subject">Asunto del mensaje</param>
	/// <param name="htmlContent">Contenido HTML</param>
	/// <returns>bool</returns>
	Task<bool> SendEmail(string recipientAddress, string subject, string htmlContent);

	#region Utility Methods
	/// <summary>
	/// Se encarga de convertir contenido html
	/// en string
	/// </summary>
	/// <param name="viewName">Nombre de la vista</param>
	/// <param name="model">Modelo de datos</param>
	/// <returns>string</returns>
	Task<string> RenderToStringAsync(string viewName, object model);
	#endregion Utility Methods
}
