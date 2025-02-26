using Catalog.Application.Features.ColorFeature.Dto;
namespace Catalog.Application.Features.ColorFeature.Queries;

public record Color_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<ColorDto>>>;
public class Color_GetAllHandler : IQueryHandler<Color_GetAllQuery, Result<IEnumerable<ColorDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Color_GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<IEnumerable<ColorDto>>> Handle(Color_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var colors = await _unitOfWork.Colors.Queryable().OrderedListQuery(orderCol, orderDir)
									  .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
									  .ToListAsync();

		return Result<IEnumerable<ColorDto>>.Success(colors);
	}
}
