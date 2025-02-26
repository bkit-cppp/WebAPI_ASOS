namespace Catalog.Application.Models.CommentModel;

public class CommentFilterRequest : FilterRequest
{
	public Guid? ParentId { get; set; }
	public Guid? ProductId { get; set; }
}
