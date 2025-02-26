using Catalog.Application.Features.ColorFeature.Dto;
using MediatR;

namespace Catalog.Application.Features.ColorFeature.Queries;

public record Color_GetBySlugQuery(string Slug) : IQuery<Result<ColorDto>>;
public class Color_GetBySlugQueryHandler : IRequestHandler<Color_GetBySlugQuery, Result<ColorDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Color_GetBySlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<ColorDto>> Handle(Color_GetBySlugQuery request, CancellationToken cancellationToken)
	{
		var slug = request.Slug;

		var color = await _unitOfWork.Colors.Queryable()
										  .Where(p => p.Slug == slug)
										  .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
										  .FirstOrDefaultAsync();

		return Result<ColorDto>.Success(color);

	}
}
