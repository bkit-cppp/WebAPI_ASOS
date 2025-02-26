using Catalog.Application.Features.UserCommentFeature.Dto;

namespace Catalog.Application.Features.CommentFeature.Dto;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid? ParentId { get; set; }
    public Guid ProductId { get; set; }
	public UserCommentDto User { get; set; }
	public DateTime? CreatedDate { get; set; }
	private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Comment, CommentDto>()
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)); ;
        }
    }
}
