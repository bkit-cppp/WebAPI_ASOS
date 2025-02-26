using Promotion.API.Features.DiscountHistoryFeature.Dto;
using BuildingBlock.Utilities;
using Promotion.API.Models;
namespace Promotion.API.Features.DiscountHistoryFeature.Queries
{
    public record DiscountHistory_GetFilterQuery(DiscountHistoryFilterRequest RequestData) : IQuery<Result<IEnumerable<DiscountHistoryDto>>>;
    public class DiscountHistory_GetFilterQueryHandler : IQueryHandler<DiscountHistory_GetFilterQuery, Result<IEnumerable<DiscountHistoryDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountHistory_GetFilterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DiscountHistoryDto>>> Handle(DiscountHistory_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.DiscountHistories.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<DiscountHistoryDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

			if (!StringHelper.GuidIsNull(request.RequestData.DiscountId))
			{
				query = query.Where(s => s.DiscountId == request.RequestData.DiscountId);
			}

			if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.DiscountApplied == decimal.Parse(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<DiscountHistoryDto>>.Success(await query.ToListAsync());
        }
    }
}
