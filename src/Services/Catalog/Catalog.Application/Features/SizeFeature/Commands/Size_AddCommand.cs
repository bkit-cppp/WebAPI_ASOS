using Catalog.Application.Features.SizeFeature.Dto;
using Catalog.Application.Models.SizeModel;
using FluentValidation;

namespace Catalog.Application.Features.SizeFeature.Commands;
public record Size_AddCommand(SizeAddOrUpdate RequestData) : ICommand<Result<SizeDto>>;

public class SizeAddCommandValidator : AbstractValidator<Size_AddCommand>
{
	public SizeAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
    }
}

public class Size_AddCommandHandler : ICommandHandler<Size_AddCommand, Result<SizeDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Sizes.IsSlugUnique(request.RequestData.Slug, true);

		var size = new Size()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
		};

		if(request.RequestData.CategoryIds != null)
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

		_unitOfWork.Sizes.Add(size, request.RequestData.CreatedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if(rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Size);
		}

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(size));
	}
}