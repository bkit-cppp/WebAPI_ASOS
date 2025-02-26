using BuildingBlock.Core.Constants;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace BuildingBlock.Core.Filters;

public class TokenExtractionAttribute : Attribute, IAsyncResourceFilter
{
	public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
	{
		var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
		if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
		{
			await next();
			return;
		}

		var token = authHeader.Substring("Bearer ".Length).Trim();
		var handler = new JwtSecurityTokenHandler();

		try
		{
			var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
			if (jsonToken == null)
			{
				await next();
				return;
			}

			var userIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == JWTClaimsTypeConstant.Id);
			if (userIdClaim != null)
			{
				context.HttpContext.Items["userId"] = userIdClaim.Value;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"*** [TokenExtractionFilter] - err: {ex.Message} ***");
		}

		await next();
	}
}
