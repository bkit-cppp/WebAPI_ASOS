using Catalog.Application.Features.ProductItemFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_AddCommand(ProductItem RequestData) : ICommand<Result<ProductItemDto>>;
public class Product_AddCommandHandler : ICommandHandler<ProductItem_AddCommand, Result<ProductItemDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Product_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public class Product_AddCommandValidator : AbstractValidator<ProductItem_AddCommand>
	{
		public Product_AddCommandValidator()
		{
			RuleFor(command => command.RequestData.ColorId)
				.NotEmpty().WithMessage("Color is required");

			RuleFor(command => command.RequestData.ProductId)
				.NotEmpty().WithMessage("Product is required");

			RuleFor(command => command.RequestData.AdditionalPrice)
				.NotNull().WithMessage("Additional price is required");
		}
	}

	public async Task<Result<ProductItemDto>> Handle(ProductItem_AddCommand request, CancellationToken cancellationToken)
	{
		var color = await _unitOfWork.Colors.FindAsync(request.RequestData.ColorId!.Value, true);

		var product = await _unitOfWork.Products.FindAsync(request.RequestData.ProductId!.Value, true);

		var productItem = new ProductItem()
		{
			ColorId = color!.Id,
			Color = color,
			ProductId = product!.Id,
			Product = product,
			AdditionalPrice = request.RequestData.AdditionalPrice
		};

		_unitOfWork.ProductItems.Add(productItem, request.RequestData.ModifiedUser);
		await _unitOfWork.CompleteAsync();

		return Result<ProductItemDto>.Success(_mapper.Map<ProductItemDto>(productItem));

	}
}
