using System.Text.Json.Serialization;

namespace GroceryStore.Common.Models.Common.BaseEntity;

/// <summary>
/// Modelo de la entidad base
/// </summary>
public class BaseModel
{
	#pragma warning disable
	public BaseModel()
    {
        
    }
	#pragma warning enable

	public BaseModel(DateTime creationDate)
	{
		CreationDate = creationDate;
	}

	/// <summary>
	/// Fecha de creacion del registro
	/// </summary>
	[JsonIgnore]
	public DateTime CreationDate
	{ get; set; }
}
