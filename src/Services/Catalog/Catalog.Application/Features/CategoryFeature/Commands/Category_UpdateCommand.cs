using Catalog.Application.Features.CategoryFeature.Dto;
using Catalog.Application.Models.CategoryModel;
using FluentValidation;

namespace Catalog.Application.Features.CategoryFeature.Commands;
public record Category_UpdateCommand(CategoryAddOrUpdateRequest RequestData) : ICommand<Result<CategoryDto>>;

public class CategoryUpdateCommandValidator : AbstractValidator<Category_UpdateCommand>
{
	public CategoryUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.Slug)
            .NotEmpty().WithMessage("Slug is required");

        RuleFor(command => command.RequestData.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}

public class Category_UpdateCommandHandler : ICommandHandler<Category_UpdateCommand, Result<CategoryDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(Category_UpdateCommand request, CancellationToken cancellationToken)
	{
		var category = await _unitOfWork.Categories.FindAsync(request.RequestData.Id!.Value, true);

		if(category!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Categories.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != category.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			category.Slug = request.RequestData.Slug;
		}

		if(request.RequestData.ParentId != null && request.RequestData.ParentId != Guid.Empty)
		{
			await _unitOfWork.Categories.SetParent(category, request.RequestData.ParentId.Value);
		}
		else
		{
			category.ParentId = null;
		}

		var ids = request.RequestData.Genders;
		if (ids != null && ids.Any())
		{
			_unitOfWork.Categories.ClearGender(category);
			var genders = await _unitOfWork.Genders.GetBatchIds(ids);
			//category.Genders = genders;
		}
		else
		{
			_unitOfWork.Categories.ClearGender(category);
		}

		category.Name = request.RequestData.Name;
		category.Description = request.RequestData.Description;
		category.ImageFile = request.RequestData.ImageFile;

		_unitOfWork.Categories.Update(category, request.RequestData.ModifiedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Category);
		}

		return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
	}
}