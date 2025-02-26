using BuildingBlock.Core.Paging;
using Identity.API.Features.RoleFeature.Dto;

namespace Identity.API.Features.RoleFeature.Queries;

public record Role_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<RoleDto>>>;
public class Role_GetPaginationQueryHandler : IQueryHandler<Role_GetPaginationQuery, Result<PaginatedList<RoleDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_GetPaginationQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<RoleDto>>> Handle(Role_GetPaginationQuery request, CancellationToken cancellationToken)
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

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<RoleDto>>.Success(paging);
	}
}