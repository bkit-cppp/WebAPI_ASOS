using Catalog.Application.Features.RatingFeature.Dto;

namespace Catalog.Application.Features.RatingFeature.Queries;

public record Rating_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<RatingDto>>>;
public class Rating_GetFilterQueryHandler : IQueryHandler<Rating_GetFilterQuery, Result<IEnumerable<RatingDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Rating_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RatingDto>>> Handle(Rating_GetFilterQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.Ratings.Queryable().OrderedListQuery(orderCol, orderDir)
                            .ProjectTo<RatingDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<RatingDto>>.Success(await query.ToListAsync());
    }
}