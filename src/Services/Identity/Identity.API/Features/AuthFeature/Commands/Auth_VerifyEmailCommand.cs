using Identity.API.Interfaces;

namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_VerifyEmailCommand(string code) : ICommand<Result<string>>;

public class Auth_VerifyEmailCommandHandler : ICommandHandler<Auth_VerifyEmailCommand, Result<string>>
{
	private readonly DataContext _context;

	public Auth_VerifyEmailCommandHandler(DataContext context)
	{
		_context = context;
	}

	public async Task<Result<string>> Handle(Auth_VerifyEmailCommand request, CancellationToken cancellationToken)
	{
		var otp = await _context.OTPs.Include(s => s.User)
						.FirstOrDefaultAsync(s => s.Code == request.code &&
												  s.Type == OTPConstant.RegisterType);
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

		otp.IsExpired = true;
		otp.User.IsEmailConfirmed = true;
		_context.Users.Update(otp.User);
		_context.OTPs.Update(otp);

		await _context.SaveChangesAsync();

		return Result<string>.Success("Valid OTP, email has been verified.");
	}
}
