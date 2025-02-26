using Catalog.Application.Features.CategoryFeature.Dto;
using Catalog.Application.Models.CategoryModel;
using FluentValidation;

namespace Catalog.Application.Features.CategoryFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/CategoryModel/CategoryAddRequest.cs
public record Category_AddCommand(CategoryAddOrUpdateRequest RequestData) : ICommand<Result<CategoryDto>>;

public class CategoryAddCommandValidator : AbstractValidator<Category_AddCommand>
{
	public CategoryAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
    }
}

public class Category_AddCommandHandler : ICommandHandler<Category_AddCommand, Result<CategoryDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(Category_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Categories.IsSlugUnique(request.RequestData.Slug, true);

		var category = new Category()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			ImageFile = request.RequestData.ImageFile
		};

		if (request.RequestData.ParentId != null && request.RequestData.ParentId != Guid.Empty)
		{
			await _unitOfWork.Categories.SetParent(category, request.RequestData.ParentId.Value);
		}

		var ids = request.RequestData.Genders;
		if (ids != null && ids.Any())
		{
			_unitOfWork.Categories.ClearGender(category);
			var genders = await _unitOfWork.Genders.GetBatchIds(ids);
			//category.Genders = genders;
		}

		_unitOfWork.Categories.Add(category, request.RequestData.CreatedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Category);
		}

		return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
	}
}