using Catalog.Application.Features.ColorFeature.Dto;

namespace Catalog.Application.Features.ColorFeature.Queries;

public record Color_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<ColorDto>>>;
public class Color_GetFilterQueryHandler : IQueryHandler<Color_GetFilterQuery, Result<IEnumerable<ColorDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Color_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<ColorDto>>> Handle(Color_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Genders.Queryable().OrderedListQuery(orderCol, orderDir)
							.ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
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

		return Result<IEnumerable<ColorDto>>.Success(await query.ToListAsync());
	}
}
