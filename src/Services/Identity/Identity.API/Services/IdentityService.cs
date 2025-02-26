using BuildingBlock.Grpc.Protos;
using Grpc.Core;

namespace Identity.API.Services;

public class IdentityService : IdentityGrpc.IdentityGrpcBase
{
	private readonly DataContext _dataContext;
	public IdentityService(DataContext dataContext)
	{
		_dataContext = dataContext;
	}
	public override async Task<GetUserReply> GetUser(GetUserRequest request, ServerCallContext context)
	{
		var user = await _dataContext.Users
						.Select(s => new GetUserReply()
						{
							Success = true,
							Id = s.Id.ToString(),
							Email = s.Email,
							Avatar = s.Avatar,
							Fullname = s.Fullname,
							Phone = s.Phone,
							RoleId = s.RoleId,
							StatusId = s.StatusId,
							Point = s.Point
						})
						.FirstOrDefaultAsync(s => s.Id == request.Id);

		if (user != null)
		{
			return user;
		}

		return new GetUserReply()
		{
			Success = false,
			ErrMessage = "User not found."
		};
	}
}