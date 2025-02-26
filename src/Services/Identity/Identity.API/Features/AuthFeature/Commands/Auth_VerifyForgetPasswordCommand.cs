using BuildingBlock.Utilities;
using Identity.API.Interfaces;

namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_VerifyForgetPasswordCommand(string code) : ICommand<Result<string>>;

public class Auth_VerifyForgetPasswordCommandHandler : ICommandHandler<Auth_VerifyForgetPasswordCommand, Result<string>>
{
	private readonly DataContext _context;

	public Auth_VerifyForgetPasswordCommandHandler(DataContext context)
	{
		_context = context;
	}

	public async Task<Result<string>> Handle(Auth_VerifyForgetPasswordCommand request, CancellationToken cancellationToken)
	{
		var otp = await _context.OTPs.Include(s => s.User)
						.FirstOrDefaultAsync(s => s.Code == request.code && 
												  s.Type == OTPConstant.ForgetPasswordType);

		if(otp is null || otp.User is null)
		{
			throw new ApplicationException($"OTP Not found: {request.code}");
		}
		
		if(DateTime.Now > otp.ValidTo)
		{
			throw new ApplicationException($"OTP was expires");
		}

		if (otp.IsExpired)
		{
			throw new ApplicationException($"OTP has already been used");
		}

		string newPassword = StringHelper.GenerateRandomString(6).ToLower();

		otp.IsExpired = true;
		otp.User.Password = newPassword;
		_context.Users.Update(otp.User);
		_context.OTPs.Update(otp);

		await _context.SaveChangesAsync();

		return Result<string>.Success($"Your new password is : {newPassword}");
	}
}
