using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Features.AuthFeature.Commands;
using Identity.API.Features.AuthFeature.Queries;
using Identity.API.Models.AuthModel;
using Microsoft.AspNetCore.Authorization;
namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : BaseController
	{
		[HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
		{
			return Ok(await Mediator.Send(new Auth_GetProfileQuery(GetUserId())));
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			request.IsAdmin = false;
			return Ok(await Mediator.Send(new Auth_LoginCommand(request)));
		}

		[HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
		{
			var request = new LogoutRequest
			{
				Id = GetUserId(),
				Token = GetToken()
			};
			return Ok(await Mediator.Send(new Auth_LogoutCommand(request)));
		}

		[HttpPost("admin/login")]
		public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
		{
			request.IsAdmin = true;
			return Ok(await Mediator.Send(new Auth_LoginCommand(request)));
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			return Ok(await Mediator.Send(new Auth_RegisterCommand(request)));
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
		{
			return Ok(await Mediator.Send(new Auth_RefreshTokenCommand(refreshToken)));
		}

		[HttpPut("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
		{
			return Ok(await Mediator.Send(new Auth_UpdatePasswordCommand(request)));
		}

		[HttpPut("forget-password")]
		public async Task<IActionResult> ForgetPassword([FromBody] string email)
		{
			return Ok(await Mediator.Send(new Auth_ForgetPasswordCommand(email)));
		}

		[HttpGet("verify-email/{code}")]
		public async Task<IActionResult> VerifyEmail([FromRoute] string code)
		{
			return Ok(await Mediator.Send(new Auth_VerifyEmailCommand(code)));
		}

		[HttpGet("verify-forget-password/{code}")]
		public async Task<IActionResult> VerifyForgetPassword([FromRoute] string code)
		{
			return Ok(await Mediator.Send(new Auth_VerifyForgetPasswordCommand(code)));
		}
	}
}
