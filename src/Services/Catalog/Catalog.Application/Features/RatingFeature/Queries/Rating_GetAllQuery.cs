using Catalog.Application.Features.RatingFeature.Dto;

namespace Catalog.Application.Features.RatingFeature.Queries;

public record Rating_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<RatingDto>>>;
public class Rating_GetAllQueryHandler : IQueryHandler<Rating_GetAllQuery, Result<IEnumerable<RatingDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Rating_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RatingDto>>> Handle(Rating_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<RatingDto> Ratings = await _unitOfWork.Genders.Queryable()
                                               .OrderedListQuery(orderCol, orderDir)
                                               .ProjectTo<RatingDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();

        return Result<IEnumerable<RatingDto>>.Success(Ratings);
    }
}