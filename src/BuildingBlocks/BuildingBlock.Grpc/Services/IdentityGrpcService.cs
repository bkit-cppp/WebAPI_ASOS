using BuildingBlock.Grpc.Protos;

namespace BuildingBlock.Grpc.Services;

public class IdentityGrpcService
{
	private readonly IdentityGrpc.IdentityGrpcClient _client;
	public IdentityGrpcService(IdentityGrpc.IdentityGrpcClient client)
	{
		_client = client;
	}

	public async Task<GetUserReply> GetUserAsync(Guid id)
	{
		var response = await _client.GetUserAsync(new GetUserRequest { Id = id.ToString() }, cancellationToken: default);
		if(response.Success != true)
		{
			throw new ApplicationException(response.ErrMessage);
		}
		if (string.IsNullOrEmpty(response.StatusId) || string.IsNullOrEmpty(response.RoleId))
		{
			throw new ApplicationException("Account is invalid");
		}
		if (response.StatusId == "BANNED")
		{
			throw new ApplicationException("Account was banned by admin");
		}
		return response;
	}
}
