using Catalog.Application.Features.GenderFeature.Dto;

namespace Catalog.Application.Features.GenderFeature.Queries;

public record Gender_GetByIdQuery(Guid Id) : IQuery<Result<GenderDto>>;
public class Gender_GetByIdQueryHandler : IQueryHandler<Gender_GetByIdQuery, Result<GenderDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GenderDto>> Handle(Gender_GetByIdQuery request, CancellationToken cancellationToken)
	{

		var gender = await _unitOfWork.Genders.Queryable()
									  .Where(s => s.Id == request.Id)
									  .ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<GenderDto>.Success(gender);
	}
}