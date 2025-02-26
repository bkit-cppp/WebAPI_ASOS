namespace Catalog.Application.Commons.Models;

public class BaseDto<TKey>
{
    public TKey Id { get; set; }
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
}
