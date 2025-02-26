using MediatR;

namespace BuildingBlock.Core.Request;

public class ExportRequest : IRequest<byte[]>
{
    public string Format { get; set; } = "Excel";
    public string Slug { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IncludeInactive { get; set; } = false;
}
