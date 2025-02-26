using FluentValidation;
using Identity.API.Models.AuthModel;
namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_LogoutCommand(LogoutRequest requestData) : ICommand<Result<bool>>;

public class Auth_LogoutCommandHandler : ICommandHandler<Auth_LogoutCommand, Result<bool>>
{
	private readonly DataContext _context;

	public Auth_LogoutCommandHandler(
		DataContext context)
	{
		_context = context;
	}

	public async Task<Result<bool>> Handle(Auth_LogoutCommand request, CancellationToken cancellationToken)
	{
		var token = await _context.Tokens.Where(s => s.AccessToken == request.requestData.Token).FirstOrDefaultAsync();

		if(token != null)
		{
			token.DeleteFlag = true;
			token.ModifiedUser = request.requestData.Id;
			token.ModifiedDate = DateTime.Now;
			_context.Update(token);
			await _context.SaveChangesAsync();
		}

		return Result<bool>.Success(true);
	}
}
