using BuildingBlock.Grpc.Protos;

namespace BuildingBlock.Grpc.Services;

public class CatalogGrpcService
{
	private readonly CatalogGrpc.CatalogGrpcClient _client;
	public CatalogGrpcService(CatalogGrpc.CatalogGrpcClient client)
	{
		_client = client;
	}

	public async Task<GetProductReply> GetProductAsync(Guid id)
	{
		var response = await _client.GetProductAsync(new GetProductRequest { Id = id.ToString() }, cancellationToken: default);
		if (response.Success != true)
		{
			throw new ApplicationException(response.ErrMessage);
		}
		if (response.QtyDisplay <= 0 || response.QtyInStock <= 0)
		{
			throw new ApplicationException("Product sold out");
		}
		return response;
	}
}