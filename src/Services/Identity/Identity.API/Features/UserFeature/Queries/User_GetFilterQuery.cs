using Identity.API.Features.UserFeature.Dto;

namespace Identity.API.Features.UserFeature.Queries;

public record User_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<UserDto>>>;
public class User_GetFilterQueryHandler : IQueryHandler<User_GetFilterQuery, Result<IEnumerable<UserDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public User_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<UserDto>>> Handle(User_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Users.OrderedListQuery(orderCol,orderDir)
							.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Email.Contains(request.RequestData.TextSearch) ||
									 s.Fullname.Contains(request.RequestData.TextSearch));
		}

		if (!string.IsNullOrEmpty(request.RequestData.RoleId))
		{
			query = query.Where(s => s.RoleId == request.RequestData.RoleId);
		}

		if (!string.IsNullOrEmpty(request.RequestData.Status))
		{
			query = query.Where(s => s.StatusId == request.RequestData.Status);
		}
		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

        return Result<IEnumerable<UserDto>>.Success(await query.ToListAsync());
	}
}