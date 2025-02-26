using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BuildingBlock.Core.Domain;

public class BaseEntity<TKey>
{
	public BaseEntity()
	{
		DeleteFlag = false;
		CreatedDate = System.DateTime.Now;
		ModifiedDate = System.DateTime.Now;
		Id = GenerateId();
	}

	[Key]
	public TKey Id { set; get; }
	[JsonIgnore] public bool DeleteFlag { set; get; }
	[JsonIgnore] public DateTime? CreatedDate { get; set; }
	[JsonIgnore] public DateTime? ModifiedDate { get; set; }
	[JsonIgnore] public Guid? CreatedUser { set; get; }
	[JsonIgnore] public Guid? ModifiedUser { set; get; }

	private TKey GenerateId()
	{
		if (typeof(TKey) == typeof(string))
		{
			return (TKey)(object)Guid.NewGuid().ToString();
		}
		else if (typeof(TKey) == typeof(Guid))
		{
			return (TKey)(object)Guid.NewGuid();
		}
		else if (typeof(TKey) == typeof(int) ||
				 typeof(TKey) == typeof(double) ||
				 typeof(TKey) == typeof(decimal) ||
				 typeof(TKey) == typeof(long))
		{
			var random = new Random();
			return (TKey)(object)random.Next(10000, 99999);
		}

		throw new InvalidOperationException("Unsupported ID type");
	}
}