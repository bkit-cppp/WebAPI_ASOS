using BuildingBlock.Core.Constants;
using BuildingBlock.Core.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace BuildingBlock.Core.Policies;

public class RolePolicyRequirement : IAuthorizationRequirement
{
	public List<string> Roles { get; }
	public RolePolicyRequirement(List<string> Roles) 
	{
		this.Roles = Roles;
	}
}


public class RoleRequirementHandler : AuthorizationHandler<RolePolicyRequirement>
{
	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePolicyRequirement requirement)
	{
		var httpContext = context.Resource as HttpContext;
		if (httpContext == null)
		{
			return;
		}

		var role = GetRole(httpContext);
		if (string.IsNullOrEmpty(role))
		{
			await ReturnResponse(httpContext);
			return;
		}

		if (requirement.Roles.Contains(role))
		{
			context.Succeed(requirement); 
		}
		return;
	}

	private async Task ReturnResponse(HttpContext httpContext)
	{
		httpContext.Response.StatusCode = StatusCodes.Status200OK;
		httpContext.Response.ContentType = "application/json";
		var response = Result<string>.Failure("You don't have permission to perform this action");
		var json = JsonSerializer.Serialize(response);
		await httpContext.Response.WriteAsync(json);
		await httpContext.Response.CompleteAsync();
	}

	protected string GetRole(HttpContext context)
	{
		try
		{
			var handler = new JwtSecurityTokenHandler();
			string authHeader = context.Request.Headers["Authorization"];
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
}