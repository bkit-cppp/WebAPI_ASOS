namespace Identity.API.Features.AuthFeature.Dto;

public class ProfileDto
{
	public Guid Id { get; set; }
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string Fullname { get; set; } = string.Empty;
	public string? Avatar { get; set; } = AvatarConstant.Default;
	public string? RoleId { get; set; }
	public string? StatusId { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<User, ProfileDto>();
		}
	}
}
