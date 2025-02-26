using Catalog.Application.Features.GenderFeature.Dto;
namespace Catalog.Application.Features.GenderFeature.Queries;

public record Gender_GetBySlugQuery(string Slug) : IQuery<Result<GenderDto>>;
public class Gender_GetBySlugQueryHandler : IQueryHandler<Gender_GetBySlugQuery, Result<GenderDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_GetBySlugQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GenderDto>> Handle(Gender_GetBySlugQuery request, CancellationToken cancellationToken)
	{

		var gender = await _unitOfWork.Genders.Queryable()
									  .Where(s => s.Slug == request.Slug)
									  .ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<GenderDto>.Success(gender);
	}
}