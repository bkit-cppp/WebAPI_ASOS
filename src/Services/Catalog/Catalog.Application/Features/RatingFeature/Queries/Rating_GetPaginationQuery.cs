using System.Linq;
using BuildingBlock.Core.Paging;
using Catalog.Application.Features.RatingFeature.Dto;

namespace Catalog.Application.Features.RatingFeature.Queries;

public record Rating_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<RatingDto>>>;
public class Rating_GetPaginationQueryHandler : IQueryHandler<Rating_GetPaginationQuery, Result<PaginatedList<RatingDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Rating_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<RatingDto>>> Handle(Rating_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.Ratings.Queryable()
                               .OrderedListQuery(orderCol, orderDir)
                               .ProjectTo<RatingDto>(_mapper.ConfigurationProvider)
                               .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Id.ToString().Contains(request.RequestData.TextSearch));
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<RatingDto>>.Success(paging);
    }
}