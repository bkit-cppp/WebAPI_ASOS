using BuildingBlock.Core.Paging;
using Identity.API.Features.UserFeature.Dto;
using Identity.API.Models.UserModel;

namespace Identity.API.Features.UserFeature.Queries;

public record User_GetPaginationQuery(UserPaginationRequest RequestData) : IQuery<Result<PaginatedList<UserDto>>>;
public class User_GetPaginationQueryHandler : IQueryHandler<User_GetPaginationQuery, Result<PaginatedList<UserDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public User_GetPaginationQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<UserDto>>> Handle(User_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Users.Include(s => s.Role).Include(s => s.Status)
							.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Email.Contains(request.RequestData.TextSearch) || 
									 s.Fullname.Contains(request.RequestData.TextSearch));
		}

		if (!string.IsNullOrEmpty(request.RequestData.Role))
		{
			query = query.Where(s => s.RoleId == request.RequestData.Role);
		}

		if (!string.IsNullOrEmpty(request.RequestData.Status))
		{
			query = query.Where(s => s.StatusId == request.RequestData.Status);
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<UserDto>>.Success(paging);
	}
}