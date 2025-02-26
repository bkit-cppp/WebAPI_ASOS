using Catalog.Application.Features.ColorFeature.Dto;
namespace Catalog.Application.Features.ColorFeature.Queries;

public record Color_GetByIdQuery(Guid Id) : IQuery<Result<ColorDto>>;
public class Color_GetByIdHandler : IQueryHandler<Color_GetByIdQuery, Result<ColorDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Color_GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<ColorDto>> Handle(Color_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var id = request.Id;

		var color = await _unitOfWork.Colors.Queryable()
									  .Where(s => s.Id == id)
									 .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
									 .SingleOrDefaultAsync();

		return Result<ColorDto>.Success(color);
	}
}

