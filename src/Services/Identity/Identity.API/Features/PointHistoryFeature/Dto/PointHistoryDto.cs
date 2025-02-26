namespace Identity.API.Features.PointHistoryFeature.Dto;

public class PointHistoryDto
{
	public Guid Id { get; set; }
	public string ReferenceId { get; set; } = string.Empty;
	public string ReferenceType { get; set; } = string.Empty;
	public int PointBefore { get; set; } = 0;
	public int PointChange { get; set; } = 0;
	public int PointAfter { get; set; } = 0;
	public string Reason { get; set; } = string.Empty;
	public Guid? UserId { get; set; }
	public DateTime? CreatedDate { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<PointHistory, PointHistoryDto>();
		}
	}
}
