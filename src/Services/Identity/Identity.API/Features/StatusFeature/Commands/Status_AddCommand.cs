using FluentValidation;
using Identity.API.Features.StatusFeature.Dto;
using Identity.API.Models.StatusModel;

namespace Identity.API.Features.StatusFeature.Commands;

public record Status_AddCommand(StatusAddOrUpdateRequest RequestData) : ICommand<Result<StatusDto>>;

public class StatusAddCommandValidator : AbstractValidator<Status_AddCommand>
{
	public StatusAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Status_AddCommandHandler : ICommandHandler<Status_AddCommand, Result<StatusDto>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Status_AddCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<StatusDto>> Handle(Status_AddCommand request, CancellationToken cancellationToken)
	{
		var exist = await _context.Statuses.FindAsync(request.RequestData.Id);

		if(exist != null)
		{
			throw new ApplicationException("Status id is already in uses");
		}

		var status = new Status()
		{
			Id = request.RequestData.Id,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			CreatedUser = request.RequestData.CreatedUser,
			ModifiedUser = request.RequestData.CreatedUser
		};

		_context.Statuses.Add(status);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<StatusDto>.Success(_mapper.Map<StatusDto>(status));
	}
}