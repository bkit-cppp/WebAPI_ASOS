using Catalog.Application.Features.BrandFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.BrandFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/BrandModel/BrandUpdateRequest.cs
public record Brand_UpdateCommand(Brand RequestData) : ICommand<Result<BrandDto>>;

public class BrandUpdateCommandValidator : AbstractValidator<Brand_UpdateCommand>
{
	public BrandUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

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

public class Brand_UpdateCommandHandler : ICommandHandler<Brand_UpdateCommand, Result<BrandDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<BrandDto>> Handle(Brand_UpdateCommand request, CancellationToken cancellationToken)
	{
		var Brand = await _unitOfWork.Brands.FindAsync(request.RequestData.Id, true);

		if(Brand!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Brands.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != Brand.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			Brand.Slug = request.RequestData.Slug;
		}

		Brand.Name = request.RequestData.Name;
		Brand.Description = request.RequestData.Description;
		Brand.Image = request.RequestData.Image;

		_unitOfWork.Brands.Update(Brand, request.RequestData.ModifiedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Brand);
		}

		return Result<BrandDto>.Success(_mapper.Map<BrandDto>(Brand));
	}
}