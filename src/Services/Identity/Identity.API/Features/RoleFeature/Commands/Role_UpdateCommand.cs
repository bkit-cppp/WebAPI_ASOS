using FluentValidation;
using Identity.API.Features.RoleFeature.Dto;

namespace Identity.API.Features.RoleFeature.Commands;

public record Role_UpdateCommand(Role RequestData) : ICommand<Result<RoleDto>>;

public class RoleUpdateCommandValidator : AbstractValidator<Role_UpdateCommand>
{
	public RoleUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Role_UpdateCommandHandler : ICommandHandler<Role_UpdateCommand, Result<RoleDto>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_UpdateCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<RoleDto>> Handle(Role_UpdateCommand request, CancellationToken cancellationToken)
	{
		var role = await _context.Roles.FindAsync(request.RequestData.Id);

		if (role == null)
		{
			throw new ApplicationException("Role not found");
		}

		role.Name = request.RequestData.Name;
		role.ModifiedDate = DateTime.Now;
		role.ModifiedUser = request.RequestData.ModifiedUser;

		_context.Roles.Update(role);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<RoleDto>.Success(_mapper.Map<RoleDto>(role));
	}
}