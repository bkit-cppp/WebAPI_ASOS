using BuildingBlock.Grpc.Protos;

namespace BuildingBlock.Grpc.Services;

public class PromotionGrpcService
{
	private readonly PromotionGrpc.PromotionGrpcClient _client;
	public PromotionGrpcService(PromotionGrpc.PromotionGrpcClient client)
	{
		_client = client;
	}

	private void Validate(GetDiscountReply? response)
	{
		if (response == null)
		{
			throw new ApplicationException("Promotion Grpc error");
		}

		if (response.Success != true)
		{
			throw new ApplicationException(response.ErrMessage);
		}

		if (!string.IsNullOrEmpty(response.StartDate))
		{
			DateTime startDate = DateTime.Parse(response.StartDate);
			if (DateTime.Now < startDate)
			{
				throw new ApplicationException($"The discount {response.Code} is not yet active.");
			}
		}

		if (!string.IsNullOrEmpty(response.EndDate))
		{
			DateTime endDate = DateTime.Parse(response.EndDate);
			if (DateTime.Now > endDate)
			{
				throw new ApplicationException($"The discount {response.Code} expired");
			}
		}

		if (response.DiscountTypeId == "Product")
		{
			List<Guid> ids = response.DiscountProducts
							.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
							.Select(id => Guid.Parse(id))
							.ToList();

			if (!ids.Any())
			{
				throw new ApplicationException($"Discount is invalid");
			}
		}
	}

	public async Task<GetDiscountReply> GetDiscountById(Guid id)
	{
		var response = await _client.GetDiscountByIdAsync
		(
			new GetDiscountByIdRequest { Id = id.ToString() }, 
			cancellationToken: default
		);

		Validate(response);

		return response;
	}

	public async Task<GetDiscountReply> GetDiscountByCode(string code)
	{
		var response = await _client.GetDiscountByCodeAsync
		(
			new GetDiscountByCodeRequest { Code = code },
			cancellationToken: default
		);

		Validate(response);

		return response;
	}
}
