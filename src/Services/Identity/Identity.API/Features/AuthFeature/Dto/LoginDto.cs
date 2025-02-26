using Identity.API.Features.UserFeature.Dto;

namespace Identity.API.Features.AuthFeature.Dto;

public class LoginDto
{
	public ProfileDto User { get; set; }
	public TokenDto Token { get; set; }
}
