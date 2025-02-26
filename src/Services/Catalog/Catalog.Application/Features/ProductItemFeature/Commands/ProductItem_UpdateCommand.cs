using Catalog.Application.Features.ProductItemFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_UpdateCommand(ProductItem RequestData) : ICommand<Result<ProductItemDto>>;
public class ProductItem_UpdateCommandValidator : AbstractValidator<ProductItem_UpdateCommand>
{
	public ProductItem_UpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.AdditionalPrice)
			.NotNull().WithMessage("Additional price is required");

		RuleFor(command => command.RequestData.ColorId)
			.NotEmpty().WithMessage("Color is required");
	}
}
public class ProductItem_UpdateCommandHandler : ICommandHandler<ProductItem_UpdateCommand, Result<ProductItemDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public ProductItem_UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<ProductItemDto>> Handle(ProductItem_UpdateCommand request, CancellationToken cancellationToken)
	{
		var productItem = await _unitOfWork.ProductItems.FindAsync(request.RequestData.Id, true);

		var color = await _unitOfWork.Colors.FindAsync(request.RequestData.ColorId!.Value, true);

		productItem!.Color = color;
		productItem!.AdditionalPrice = request.RequestData.AdditionalPrice;

		_unitOfWork.ProductItems.Update(productItem, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<ProductItemDto>.Success(_mapper.Map<ProductItemDto>(productItem));
	}
}
