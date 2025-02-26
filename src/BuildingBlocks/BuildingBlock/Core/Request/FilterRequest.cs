namespace BuildingBlock.Core.Request;

public class FilterRequest : BaseRequest
{
	public int? Skip { get; set; }
	public int? TotalRecord { get; set; }
}
