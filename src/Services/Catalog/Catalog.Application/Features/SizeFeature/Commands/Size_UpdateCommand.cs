using Catalog.Application.Features.SizeFeature.Dto;
using Catalog.Application.Models.SizeModel;
using FluentValidation;

namespace Catalog.Application.Features.SizeFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/SizeModel/SizeUpdateRequest.cs
public record Size_UpdateCommand(SizeAddOrUpdate RequestData) : ICommand<Result<SizeDto>>;

public class SizeUpdateCommandValidator : AbstractValidator<Size_UpdateCommand>
{
	public SizeUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
    }
}

public class Size_UpdateCommandHandler : ICommandHandler<Size_UpdateCommand, Result<SizeDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_UpdateCommand request, CancellationToken cancellationToken)
	{
		var size = await _unitOfWork.Sizes.Queryable()
						 .Include(s => s.SizeCategories)
						 .FirstOrDefaultAsync(s => s.Id == request.RequestData.Id!.Value);

		if(size == null)
		{
			throw new ApplicationException($"Data not found: {request.RequestData.Id!.Value}");
		}

		if(size!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Sizes.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != size.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			size.Slug = request.RequestData.Slug;
		}

		size.Name = request.RequestData.Name;
		size.Description = request.RequestData.Description;
		
		if(size.SizeCategories != null)
		{
			_unitOfWork.SizeCategories.DeleteRange(size.SizeCategories.ToList());
			if (request.RequestData.CategoryIds != null)
			{
				foreach (var id in request.RequestData.CategoryIds)
				{
					var categories = new SizeCategory()
					{
						CategoryId = id,
						SizeId = size.Id,
					};
					_unitOfWork.SizeCategories.Add(categories, request.RequestData.CreatedUser);
				}
			}
		}

		_unitOfWork.Sizes.Update(size, request.RequestData.ModifiedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Size);
		}

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(size));
	}
}