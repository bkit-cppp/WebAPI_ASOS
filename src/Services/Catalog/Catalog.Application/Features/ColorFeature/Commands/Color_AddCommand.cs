using Catalog.Application.Features.ColorFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.ColorFeature.Commands;

public record Color_AddCommand(Color RequestData) : ICommand<Result<ColorDto>>;
public class ColorAddCommandValidator : AbstractValidator<Color_AddCommand>
{
	public ColorAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

		RuleFor(command => command.RequestData.Description)
			.NotEmpty().WithMessage("Description is required");
	}
}
public class Color_AddCommandHandler : ICommandHandler<Color_AddCommand, Result<ColorDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Color_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<ColorDto>> Handle(Color_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Colors.IsSlugUnique(request.RequestData.Slug, true);
		var color = new Color()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description
		};

		_unitOfWork.Colors.Add(color, request.RequestData.CreatedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Color);
		}

		return Result<ColorDto>.Success(_mapper.Map<ColorDto>(color));
	}
}

