using GroceryStore.Common.DTOS.Email;
using GroceryStore.Common.Models.Common.GlobalResponse;

namespace GroceryStore.Application.Interfaces.Email;

/// <summary>
/// Define los metodos para el manejo de envio de correos
/// </summary>
public interface IEmailRepository
{
	#region Post Methods
	/// <summary>
	/// Se encarga de gestionar el envio de correos
	/// haciendo uso del servicio de envio de Azure
	/// </summary>
	/// <param name="email">Datos para el envio de correos</param>
	/// <returns>ReturnResponses</returns>
	Task<ReturnResponses> ManageEmailSend(EmailDto email);
	#endregion Post Methods
}
