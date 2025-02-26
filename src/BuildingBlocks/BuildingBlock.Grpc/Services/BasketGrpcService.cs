using BuildingBlock.Grpc.Protos;

namespace BuildingBlock.Grpc.Services;

public class BasketGrpcService
{
	private readonly BasketGrpc.BasketGrpcClient _client;
	public BasketGrpcService(BasketGrpc.BasketGrpcClient client)
	{
		_client = client;
	}

	public async Task<decimal> GetProductAsync(Guid user)
	{
		var response = await _client.GetTotalAsync(new GetCartTotalRequest { User = user.ToString() }, cancellationToken: default);
		if (response.Success != true)
		{
			throw new ApplicationException(response.ErrMessage);
		}
		return (decimal)response.Total;
	}

	public async Task<GetCartResponse> GetCartAsync(Guid user)
	{
		var response = await _client.GetCartAsync(new GetCartRequest { User = user.ToString() }, cancellationToken: default);
		if (response.Success != true)
		{
			throw new ApplicationException(response.ErrMessage);
		}
		return response;
	}
}