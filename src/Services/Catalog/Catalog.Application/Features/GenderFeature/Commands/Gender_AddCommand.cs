using Catalog.Application.Features.GenderFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.GenderFeature.Commands;

public record Gender_AddCommand(Gender RequestData) : ICommand<Result<GenderDto>>;

public class GenderAddCommandValidator : AbstractValidator<Gender_AddCommand>
{
	public GenderAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Gender_AddCommandHandler : ICommandHandler<Gender_AddCommand, Result<GenderDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GenderDto>> Handle(Gender_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Genders.IsSlugUnique(request.RequestData.Slug, true);

		var gender = new Gender()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description
		};

		_unitOfWork.Genders.Add(gender, request.RequestData.CreatedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Gender);
		}

		return Result<GenderDto>.Success(_mapper.Map<GenderDto>(gender));
	}
}