using BuildingBlock.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace BuildingBlock.Core.WebApi
{
	[ApiController]
	public abstract class BaseController : Controller
	{
		private IMediator _mediator;
		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

		protected Guid GetUserId()
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();
				string authHeader = HttpContext.Request.Headers["Authorization"];
				if (string.IsNullOrEmpty(authHeader)) return Guid.Empty;
				authHeader = authHeader.Replace("Bearer ", "");
				var jsonToken = handler.ReadToken(authHeader);
				var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
				if (tokenS == null) { return Guid.Empty; }
				var id = tokenS.Claims.First(claim => claim.Type == JWTClaimsTypeConstant.Id).Value;
				return Guid.Parse(id);
			}
			catch
			{
				return Guid.Empty;
			}
		}

		protected string GetRole()
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();
				string authHeader = HttpContext.Request.Headers["Authorization"];
				if (string.IsNullOrEmpty(authHeader)) return "";
				authHeader = authHeader.Replace("Bearer ", "");
				var jsonToken = handler.ReadToken(authHeader);
				var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
				if (tokenS == null) { return ""; }
				var id = tokenS.Claims.First(claim => claim.Type == JWTClaimsTypeConstant.Role).Value;
				return id;
			}
			catch
			{
				return "";
			}
		}

		protected string GetToken()
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();
				string authHeader = HttpContext.Request.Headers["Authorization"];
				if (string.IsNullOrEmpty(authHeader)) return "";
				authHeader = authHeader.Replace("Bearer ", "");
				return authHeader;
			}
			catch
			{
				return "";
			}
		}

		protected void SetCookie(HttpResponse Response, string accessToken, string refreshToken)
		{
			Response.Cookies.Append(JWTConstant.AccessTokenCookie, accessToken, new CookieOptions
			{
				HttpOnly = false,
				Secure = true,
				SameSite = SameSiteMode.None,
				Expires = JWTConstant.ValidTo()
			});

			Response.Cookies.Append(JWTConstant.RefreshTokenCookie, refreshToken, new CookieOptions
			{
				HttpOnly = false,
				Secure = true,
				SameSite = SameSiteMode.None,
				Expires = JWTConstant.RefreshTokenValidTo()
			});
		}
	}
}
