using Catalog.Application.Features.VariationFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.VariationFeature.Commands;

public record Variation_AddCommand(Variation RequestData) : ICommand<Result<VariationDto>>;
public class VariationAddCommandValidator : AbstractValidator<Variation_AddCommand>
{
	public VariationAddCommandValidator()
	{
		RuleFor(command => command.RequestData.QtyDisplay)
			.NotEmpty().WithMessage("Quantity display is required");

		RuleFor(command => command.RequestData.QtyInStock)
			.NotEmpty().WithMessage("Quantity in stock is required");

		RuleFor(command => command.RequestData.SizeId)
			.NotEmpty().WithMessage("Size is required");

		RuleFor(command => command.RequestData.ProductItemId)
			.NotEmpty().WithMessage("Product item is required");
	}
}
public class Color_AddCommandHandler : ICommandHandler<Variation_AddCommand, Result<VariationDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Color_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<VariationDto>> Handle(Variation_AddCommand request, CancellationToken cancellationToken)
	{
		var size = await _unitOfWork.Sizes.FindAsync(request.RequestData.SizeId!.Value, true);
		var product = await _unitOfWork.ProductItems.FindAsync(request.RequestData.ProductItemId!.Value, true);

		var variations = new Variation()
		{
			QtyDisplay = request.RequestData.QtyDisplay,
			QtyInStock = request.RequestData.QtyInStock,
			Stock = request.RequestData.Stock,
			Size = size,
			SizeId = size!.Id,
			ProductItem = product,
			ProductItemId = product!.Id
		};

		_unitOfWork.Variations.Add(variations, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<VariationDto>.Success(_mapper.Map<VariationDto>(variations));
	}
}
