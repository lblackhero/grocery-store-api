namespace GroceryStore.Common.Functionalities;

/// <summary>
/// Implementacion de los metodos de funcionalidades generales
/// </summary>
public class Functionalities : IFunctionalities
{
	/// <summary>
	/// Obtiene la hora del servidor
	/// </summary>
	/// <returns>DateTime</returns>
	public DateTime GetTimeColombia()
		=> TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")).DateTime;
}
