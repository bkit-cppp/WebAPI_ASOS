namespace Catalog.Application.Features.UserCommentFeature.Dto;

public  class UserCommentDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
	public string Avatar { get; set; } = string.Empty;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<UserComment, UserCommentDto>();
		}
	}
}
