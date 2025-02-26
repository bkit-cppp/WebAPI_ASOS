using Catalog.Application.Features.GenderFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.GenderFeature.Commands;

public record Gender_UpdateCommand(Gender RequestData) : ICommand<Result<GenderDto>>;

public class GenderUpdateCommandValidator : AbstractValidator<Gender_UpdateCommand>
{
	public GenderUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Gender_UpdateCommandHandler : ICommandHandler<Gender_UpdateCommand, Result<GenderDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GenderDto>> Handle(Gender_UpdateCommand request, CancellationToken cancellationToken)
	{
		var gender = await _unitOfWork.Genders.FindAsync(request.RequestData.Id, true);

		if(gender!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Genders.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != gender.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			gender.Slug = request.RequestData.Slug;
		}

		gender.Name = request.RequestData.Name;
		gender.Description = request.RequestData.Description;

		_unitOfWork.Genders.Update(gender, request.RequestData.ModifiedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Gender);
		}

		return Result<GenderDto>.Success(_mapper.Map<GenderDto>(gender));
	}
}