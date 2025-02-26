using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Models.ProductModel;
using FluentValidation;

namespace Catalog.Application.Features.ProductFeature.Commands;

public record Product_UpdateCommand(ProductAddOrUpdate RequestData) : ICommand<Result<ProductDto>>;
public class Product_UpdateCommandValidator : AbstractValidator<Product_UpdateCommand>
{
	public Product_UpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

		RuleFor(command => command.RequestData.Description)
			.NotEmpty().WithMessage("Description is required");

		RuleFor(command => command.RequestData.Image)
			.NotEmpty().WithMessage("Image is required");

		RuleFor(command => command.RequestData.BrandId)
			.NotEmpty().WithMessage("Brand is required");

		RuleFor(command => command.RequestData.CategoryId)
				.NotEmpty().WithMessage("Category is required");

		RuleFor(command => command.RequestData.GenderId)
			.NotEmpty().WithMessage("Gender is required");

		RuleFor(command => command.RequestData.OriginalPrice)
			.NotEmpty().WithMessage("Original price is required");

		RuleFor(command => command.RequestData.SalePrice)
			.NotEmpty().WithMessage("Sale price is required");

		RuleFor(command => command.RequestData.IsSale)
			.NotNull().WithMessage("Is sale is required");

	}
}
public class Product_UpdateCommandHandler : ICommandHandler<Product_UpdateCommand, Result<ProductDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Product_UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<ProductDto>> Handle(Product_UpdateCommand request, CancellationToken cancellationToken)
	{
		var product = await _unitOfWork.Products.FindAsync(request.RequestData.Id!.Value, true);

		if (product!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Products.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != product.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			product.Slug = request.RequestData.Slug;
		}

		product.Name = request.RequestData.Name;
		product.Description = request.RequestData.Description;
		product.SizeAndFit = request.RequestData.SizeAndFit;
		product.Image = request.RequestData.Image;
		product.OriginalPrice = request.RequestData.OriginalPrice;
		product.SalePrice = request.RequestData.SalePrice;
		product.IsSale = request.RequestData.IsSale;

		if (request.RequestData.BrandId!.Value != product.BrandId)
		{
			product.Brand = await _unitOfWork.Brands.FindAsync(request.RequestData.BrandId!.Value, true);
		}

		var categoryGender = await _unitOfWork.CategoryGenders.Queryable()
								   .Where(s => s.CategoryId == request.RequestData.CategoryId &&
											   s.GenderId == request.RequestData.GenderId)
								   .FirstOrDefaultAsync();

		if (categoryGender == null)
		{
			throw new ApplicationException("Gender and category is invalid");
		}

		if (categoryGender.Id != product.CategoryGenderId)
		{
			product.CategoryGender = categoryGender;
		}

		_unitOfWork.Products.Update(product, request.RequestData.ModifiedUser);
		await _unitOfWork.CompleteAsync();

		return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
	}
}
