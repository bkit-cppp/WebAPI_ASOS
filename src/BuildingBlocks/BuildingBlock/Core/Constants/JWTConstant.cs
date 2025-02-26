namespace BuildingBlock.Core.Constants;

public static class JWTConstant
{
	public const string ValidAudience = "Asos_Web_Application";
	public const string ValidIssuer = "Asos_Web_Application";
	public const string Secret = "my-secrect-for-Asos_web_application";
	public const string AccessTokenCookie = "Asos-access-token-cookie";
	public const string RefreshTokenCookie = "Asos-refresh-token-cookie";
	public static DateTime ValidTo()
	{
		return DateTime.Now.AddMinutes(30);
	}

	public static DateTime RefreshTokenValidTo()
	{
		return DateTime.Now.AddHours(2);
	}
}
