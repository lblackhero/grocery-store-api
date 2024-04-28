namespace GroceryStore.Common.DTOS.Email;

/// <summary>
/// DTO para el envio de correos
/// </summary>
/// <param name="receiver">A quien se le envia el correo</param>
/// <param name="subject">Asunto del correo</param>
/// <param name="viewNameTemplate">Nombre de la vista</param>
/// <param name="model">Modelo</param>
public class EmailDto(string receiver, string subject, string viewNameTemplate, object model)
{
    /// <summary>
    /// A quien se le envia
    /// </summary>
    public string Receiver
    { get; set; } = receiver;

    /// <summary>
    /// Asunto
    /// </summary>
    public string Subject
    { get; set; } = subject;

    /// <summary>
    /// Nombre de la vista
    /// </summary>
    public string ViewNameTemplate
    { get; set; } = string.Concat(viewNameTemplate, ".cshtml");

    /// <summary>
    /// Modelo
    /// </summary>
    public object Model
    { get; set; } = model;
}
