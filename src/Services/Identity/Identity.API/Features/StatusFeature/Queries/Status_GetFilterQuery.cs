using Identity.API.Features.StatusFeature.Dto;

namespace Identity.API.Features.StatusFeature.Queries;

public record Status_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<StatusDto>>>;
public class Status_GetFilterQueryHandler : IQueryHandler<Status_GetFilterQuery, Result<IEnumerable<StatusDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Status_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<StatusDto>>> Handle(Status_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Statuses.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<StatusDto>(_mapper.ConfigurationProvider)
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

		return Result<IEnumerable<StatusDto>>.Success(await query.ToListAsync());
	}
}