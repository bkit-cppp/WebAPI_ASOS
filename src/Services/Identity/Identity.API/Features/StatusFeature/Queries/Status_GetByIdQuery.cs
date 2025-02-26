using Identity.API.Features.StatusFeature.Dto;

namespace Identity.API.Features.StatusFeature.Queries;

public record Status_GetByIdQuery(string Id) : IQuery<Result<StatusDto>>;
public class Status_GetByIdQueryHandler : IQueryHandler<Status_GetByIdQuery, Result<StatusDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Status_GetByIdQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<StatusDto>> Handle(Status_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var Statuss = await _context.Statuses.Where(s => s.Id == request.Id)
								    .ProjectTo<StatusDto>(_mapper.ConfigurationProvider)
								    .FirstOrDefaultAsync();
		return Result<StatusDto>.Success(Statuss);
	}
}