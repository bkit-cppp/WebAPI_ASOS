using BuildingBlock.Grpc.Protos;
using Grpc.Core;

namespace Promotion.API.Services;

public class PromotionService : PromotionGrpc.PromotionGrpcBase
{
	private readonly DataContext _dataContext;
	public PromotionService(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public override async Task<GetDiscountReply> GetDiscountById(GetDiscountByIdRequest request, ServerCallContext context)
	{
		var discount = await _dataContext.Discounts
							.Where(s => s.DiscountTypeId != null)
							.Select(s => new GetDiscountReply
							{
								Success = true,
								ErrMessage = string.Empty,
								Id = s.Id.ToString(),
								Code = s.Code,
								Value = (double)s.Value,
								Minimum = (double)s.Minimum,
								StartDate = s.StartDate.ToString("o"),
								EndDate = s.EndDate.ToString("o"),
								Condition = s.Condition,
								Quantity = s.Quantity,
								Available = s.Available,
								DiscountTypeId = s.DiscountTypeId ?? string.Empty,
								DiscountProducts = s.DiscountProducts == null ? string.Empty :
								string.Join(",", s.DiscountProducts.Select(p => p.Id.ToString())),
							})
							.FirstOrDefaultAsync(s => s.Id == request.Id);

		if (discount == null)
		{
			return new GetDiscountReply()
			{
				Success = false,
				ErrMessage = "Discount not found."
			};
		}

		if (discount.Available == false)
		{
			return new GetDiscountReply()
			{
				Success = false,
				ErrMessage = "Discount is not available."
			};
		}

		return discount;
	}

	public override async Task<GetDiscountReply> GetDiscountByCode(GetDiscountByCodeRequest request, ServerCallContext context)
	{
		var discount = await _dataContext.Discounts
							.Select(s => new GetDiscountReply
							{
								Success = true,
								ErrMessage = string.Empty,
								Id = s.Id.ToString(),
								Code = s.Code,
								Value = (double)s.Value,
								Minimum = (double)s.Minimum,
								StartDate = s.StartDate.ToString("o"),
								EndDate = s.EndDate.ToString("o"),
								DiscountTypeId = s.DiscountTypeId ?? string.Empty,
								DiscountProducts = s.DiscountProducts == null ? string.Empty :
								string.Join(",", s.DiscountProducts.Select(p => p.Id.ToString()))
							})
							.FirstOrDefaultAsync(s => s.Code == request.Code);

		if (discount == null)
		{
			return new GetDiscountReply()
			{
				Success = false,
				ErrMessage = "Discount not found."
			};

		}

		return discount;
	}
}