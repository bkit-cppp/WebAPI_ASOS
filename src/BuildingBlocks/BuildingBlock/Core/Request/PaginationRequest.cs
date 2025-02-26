namespace BuildingBlock.Core.Request;

public class PaginationRequest : BaseRequest
{
	public int PageIndex { get; init; }
	public int PageSize { get; init; }
}
