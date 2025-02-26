using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Models.ProductModel;
using FluentValidation;

namespace Catalog.Application.Features.ProductFeature.Commands;

public record Product_AddCommand(ProductAddOrUpdate RequestData) : ICommand<Result<ProductDto>>;
public class Product_AddCommandHandler : ICommandHandler<Product_AddCommand, Result<ProductDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Product_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	public class Product_AddCommandValidator : AbstractValidator<Product_AddCommand>
	{
		public Product_AddCommandValidator()
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
	public async Task<Result<ProductDto>> Handle(Product_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Products.IsSlugUnique(request.RequestData.Slug, true);

		var product = new Product()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			SizeAndFit = request.RequestData.SizeAndFit,
			Image = request.RequestData.Image,
			OriginalPrice = request.RequestData.OriginalPrice,
			SalePrice = request.RequestData.SalePrice,
			IsSale = request.RequestData.IsSale,
			AverageRating = 0,
			Bought = 0
		};

		product.Brand = await _unitOfWork.Brands.FindAsync(request.RequestData.BrandId!.Value, true);

		var categoryGender = await _unitOfWork.CategoryGenders.Queryable()
								   .Where(s => s.CategoryId == request.RequestData.CategoryId &&
											   s.GenderId == request.RequestData.GenderId)
								   .FirstOrDefaultAsync();

		if(categoryGender == null)
		{
			throw new ApplicationException("Gender and category is invalid");
		}

		product.CategoryGender = categoryGender;

		_unitOfWork.Products.Add(product, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
	}
}
