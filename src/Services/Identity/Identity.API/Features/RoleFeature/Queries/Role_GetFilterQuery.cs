using Identity.API.Features.RoleFeature.Dto;

namespace Identity.API.Features.RoleFeature.Queries;

public record Role_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<RoleDto>>>;
public class Role_GetFilterQueryHandler : IQueryHandler<Role_GetFilterQuery, Result<IEnumerable<RoleDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<RoleDto>>> Handle(Role_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Roles.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<RoleDto>>.Success(await query.ToListAsync());
	}
}
