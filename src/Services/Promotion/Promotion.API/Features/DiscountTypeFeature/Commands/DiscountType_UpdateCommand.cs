using Promotion.API.Features.DiscountTypeFeature.Dto;
using Promotion.API.Models;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountTypeFeature.Commands;

public record DiscountType_UpdateCommand(DiscountTypeAddOrUpdateRequest RequestData) : ICommand<Result<DiscountTypeDto>>;
public class DiscountType_UpdateCommandHandler : ICommandHandler<DiscountType_CreateCommand, Result<DiscountTypeDto>>
{
	private readonly DataContext _context;

	public DiscountType_UpdateCommandHandler(DataContext context)
	{
		_context = context;
	}
	public async Task<Result<DiscountTypeDto>> Handle(DiscountType_CreateCommand request, CancellationToken cancellationToken)
	{
		var discount_type = new DiscountType();
		discount_type.Name = request.RequestData.Name;
		discount_type.Description = request.RequestData.Description;
		_context.DiscountTypes.Update(discount_type);
		int rows = await _context.SaveChangesAsync();
		if (rows > 0)
		{
			// Trả về DiscountDto nếu lưu thành công
			var d_tDto = new DiscountTypeDto
			{
				Name = discount_type.Name,
				Description = discount_type.Description
			};

			return Result<DiscountTypeDto>.Success(d_tDto);
		}
		else
		{
			// Trả về lỗi nếu không lưu được
			return Result<DiscountTypeDto>.Failure("Failed to create discount.");
		}
	}
}
