using Catalog.Application.Features.BrandFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.BrandFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/BrandModel/BrandAddRequest.cs
public record Brand_AddCommand(Brand RequestData) : ICommand<Result<BrandDto>>;

public class BrandAddCommandValidator : AbstractValidator<Brand_AddCommand>
{
	public BrandAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

        RuleFor(command => command.RequestData.Image)
            .NotEmpty().WithMessage("Image is required");

        RuleFor(command => command.RequestData.Description)
            .NotEmpty().WithMessage("Description is required");

    }
}

public class Brand_AddCommandHandler : ICommandHandler<Brand_AddCommand, Result<BrandDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<BrandDto>> Handle(Brand_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Brands.IsSlugUnique(request.RequestData.Slug, true);

		var Brand = new Brand()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			Image = request.RequestData.Image
		};

		_unitOfWork.Brands.Add(Brand, request.RequestData.CreatedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if(rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Brand);
		}

		return Result<BrandDto>.Success(_mapper.Map<BrandDto>(Brand));
	}
}