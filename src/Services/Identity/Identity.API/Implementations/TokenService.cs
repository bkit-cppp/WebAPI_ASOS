using BuildingBlock.Core.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.API.Features.AuthFeature.Dto;
using Identity.API.Interfaces;
using System.Security.Cryptography;

namespace Identity.API.Implementations;

public class TokenService : ITokenService
{
	private readonly DataContext _context;
	public TokenService(DataContext context)
	{
		_context = context;
	}

	private string GenerateAccessToken(Guid id, string email, string fullname, string avatar, string address,string phone, string role, string status)
	{
		var authClaims = new[]
		{
			new Claim(JWTClaimsTypeConstant.Id, id.ToString()),
			new Claim(JWTClaimsTypeConstant.Email, email),
			new Claim(JWTClaimsTypeConstant.Fullname, fullname),
			new Claim(JWTClaimsTypeConstant.Avatar, avatar),
			new Claim(JWTClaimsTypeConstant.Address, address),
			new Claim(JWTClaimsTypeConstant.Phone, phone),
			new Claim(JWTClaimsTypeConstant.Role, role),
			new Claim(JWTClaimsTypeConstant.Status, status),
		};

		SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(JWTConstant.Secret));

		JwtSecurityToken token = new(
			JWTConstant.ValidIssuer,
			JWTConstant.ValidAudience,
			expires: JWTConstant.ValidTo(),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	private string GenerateRefreshToken(int size = 32)
	{
		byte[] randomNumber = new byte[size];

		using (var rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(randomNumber);
		}

		return Convert.ToBase64String(randomNumber);
	}

	public async Task<TokenDto> GenerateToken(Guid id, string email, string fullname, string avatar, string address,string phone, string role, string status)
	{
		TokenDto dto = new TokenDto
		{
			AccessToken = GenerateAccessToken(id, email,fullname,avatar,address,phone,role,status),
			RefreshToken = GenerateRefreshToken(),
			AccessTokenValidTo = JWTConstant.ValidTo(),
			RefreshTokenValidTo = JWTConstant.RefreshTokenValidTo()
		};
		await Create(id, dto);
		return dto;
	}

	private async Task<Token> Create(Guid userId, TokenDto tokenDto)
	{
		var token = new Token()
		{
			Id = Guid.NewGuid(),
			AccessToken = tokenDto.AccessToken,
			RefreshToken = tokenDto.RefreshToken,
			ExpireAt = tokenDto.RefreshTokenValidTo,
			UserId = userId,
			CreatedDate = DateTime.Now,
			CreatedUser = userId,
		};
		_context.Tokens.Add(token);
		await _context.SaveChangesAsync();
		return token;
	}
}
