using Identity.API.Features.RoleFeature.Dto;

namespace Identity.API.Features.RoleFeature.Queries;

public record Role_GetByIdQuery(string Id) : IQuery<Result<RoleDto>>;
public class Role_GetByIdQueryHandler : IQueryHandler<Role_GetByIdQuery, Result<RoleDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_GetByIdQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<RoleDto>> Handle(Role_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var Roles = await _context.Roles.Where(s => s.Id == request.Id)
								  .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
								  .FirstOrDefaultAsync();
		return Result<RoleDto>.Success(Roles);
	}
}