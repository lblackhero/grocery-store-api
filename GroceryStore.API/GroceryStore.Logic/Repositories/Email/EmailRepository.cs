using GroceryStore.Application.Interfaces.Email;
using GroceryStore.AzureCommunicationEmail.AzureEmailService;
using GroceryStore.Common.DTOS.Email;
using GroceryStore.Common.Models.Common.GlobalResponse;
using GroceryStore.Common.Statics.Common;
using GroceryStore.Common.Statics.Email;
using System.Net;

namespace GroceryStore.Logic.Repositories.Email;

public class EmailRepository(IAzureEmailService azureEmailService) : IEmailRepository
{
	private readonly IAzureEmailService AzureEmailService = azureEmailService;

	#region Post Methods
	/// <summary>
	/// Se encarga de gestionar el envio de correos
	/// haciendo uso del servicio de envio de Azure
	/// </summary>
	/// <param name="email">Datos para el envio de correos</param>
	/// <returns>ReturnResponses</returns>
	public async Task<ReturnResponses> ManageEmailSend(EmailDto email)
	{
		try
		{
			string htmlContent = await AzureEmailService.RenderToStringAsync(string.Concat(EmailStatics.ConfirmationOderRoute, email.ViewNameTemplate), email.Model);
			bool isEmailSend = await AzureEmailService.SendEmailAsync(email.Receiver, email.Subject, htmlContent);

			if (!isEmailSend)
				return new(HttpStatusCode.BadRequest, ResponseMessage: $"An error occurred while trying to sent the order confirmation email to: {email.Receiver}");

			return new(HttpStatusCode.OK, ResponseMessage: GenericResponse.GenericOkMessage);
		}
		catch 
		{
			return new(HttpStatusCode.BadRequest, ResponseMessage: $"An error occurred while trying to sent the order confirmation email to: {email.Receiver}");
		}
	}
	#endregion Post Methods

}
