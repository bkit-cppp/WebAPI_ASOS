using BuildingBlock.Core.Paging;
using Promotion.API.Features.DiscountFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountFeature.Queries
{
    public record Discount_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<DiscountDto>>>;
    public class Discount_GetPaginationQueryHandler : IQueryHandler<Discount_GetPaginationQuery, Result<PaginatedList<DiscountDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Discount_GetPaginationQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<DiscountDto>>> Handle(Discount_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Discounts.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

			if (!string.IsNullOrEmpty(request.RequestData.Type))
			{
				query = query.Where(s => s.DiscountTypeId == request.RequestData.Type);
			}

			if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
			{
				query = query.Where(s => s.Code.Contains(request.RequestData.TextSearch));
			}

			var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
            return Result<PaginatedList<DiscountDto>>.Success(paging);
        }
    }
}
