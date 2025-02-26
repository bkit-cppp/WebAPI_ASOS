using FluentValidation;
using Identity.API.Features.RoleFeature.Dto;
namespace Identity.API.Features.RoleFeature.Commands;

public record Role_AddCommand(Role RequestData) : ICommand<Result<RoleDto>>;

public class RoleAddCommandValidator : AbstractValidator<Role_AddCommand>
{
	public RoleAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Role_AddCommandHandler : ICommandHandler<Role_AddCommand, Result<RoleDto>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_AddCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<RoleDto>> Handle(Role_AddCommand request, CancellationToken cancellationToken)
	{
		var exist = await _context.Roles.FindAsync(request.RequestData.Id);

		if (exist != null)
		{
			throw new ApplicationException("Role id is already in uses");
		}

		var Role = new Role()
		{
			Id = request.RequestData.Id,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			CreatedUser = request.RequestData.CreatedUser,
			ModifiedUser = request.RequestData.CreatedUser
		};

		_context.Roles.Add(Role);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<RoleDto>.Success(_mapper.Map<RoleDto>(Role));
	}
}