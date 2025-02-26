using Identity.API.Features.RoleFeature.Dto;

namespace Identity.API.Features.RoleFeature.Queries;

public record Role_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<RoleDto>>>;
public class Role_GetAllQueryHandler : IQueryHandler<Role_GetAllQuery, Result<IEnumerable<RoleDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_GetAllQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<RoleDto>>> Handle(Role_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<RoleDto> Roles = await _context.Roles.OrderedListQuery(orderCol, orderDir)
												   .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
												   .ToListAsync();

		return Result<IEnumerable<RoleDto>>.Success(Roles);
	}
}