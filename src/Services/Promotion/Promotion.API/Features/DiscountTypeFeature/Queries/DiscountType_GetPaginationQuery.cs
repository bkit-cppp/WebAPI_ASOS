using BuildingBlock.Core.Paging;
using Promotion.API.Features.DiscountTypeFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountTypeFeature.Queries
{
    public record DiscountType_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<DiscountTypeDto>>>;
    public class DiscountType_GetPaginationQueryHandler : IQueryHandler<DiscountType_GetPaginationQuery, Result<PaginatedList<DiscountTypeDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountType_GetPaginationQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<DiscountTypeDto>>> Handle(DiscountType_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.DiscountTypes.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<DiscountTypeDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
            return Result<PaginatedList<DiscountTypeDto>>.Success(paging);
        }
    }
}
