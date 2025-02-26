using BuildingBlock.Core.Paging;
using Promotion.API.Features.DiscountProductFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountProductFeature.Queries
{
    public record DiscountProduct_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<DiscountProductDto>>>;
    public class DiscountProduct_GetPaginationQueryHandler : IQueryHandler<DiscountProduct_GetPaginationQuery, Result<PaginatedList<DiscountProductDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountProduct_GetPaginationQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<DiscountProductDto>>> Handle(DiscountProduct_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.DiscountProducts.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<DiscountProductDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
            return Result<PaginatedList<DiscountProductDto>>.Success(paging);
        }
    }
}
