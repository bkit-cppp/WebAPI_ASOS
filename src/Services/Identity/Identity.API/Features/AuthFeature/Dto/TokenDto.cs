namespace Identity.API.Features.AuthFeature.Dto;

public class TokenDto
{
	public string AccessToken { get; set; }
	public string RefreshToken { get; set; }
	public DateTime AccessTokenValidTo { get; set; }
	public DateTime RefreshTokenValidTo { get; set; }
}
