using FluentValidation;
using Identity.API.Features.StatusFeature.Dto;
using Identity.API.Models.StatusModel;

namespace Identity.API.Features.StatusFeature.Commands;

public record Status_UpdateCommand(StatusAddOrUpdateRequest RequestData) : ICommand<Result<StatusDto>>;

public class StatusUpdateCommandValidator : AbstractValidator<Status_UpdateCommand>
{
	public StatusUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Status_UpdateCommandHandler : ICommandHandler<Status_UpdateCommand, Result<StatusDto>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Status_UpdateCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<StatusDto>> Handle(Status_UpdateCommand request, CancellationToken cancellationToken)
	{
		var status = await _context.Statuses.FindAsync(request.RequestData.Id);

		if (status == null)
		{
			throw new ApplicationException("Status not found");
		}

		status.Name = request.RequestData.Name;
		status.Description = request.RequestData.Description;
		status.ModifiedDate = DateTime.Now;
		status.ModifiedUser = request.RequestData.ModifiedUser;

		_context.Statuses.Update(status);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<StatusDto>.Success(_mapper.Map<StatusDto>(status));
	}
}