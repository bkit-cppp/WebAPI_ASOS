using Promotion.API.Features.DiscountHistoryFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountHistoryFeature.Queries
{
    public record DiscountHistory_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<DiscountHistoryDto>>>;
    public class DiscountHistory_GetAllQueryHandler : IQueryHandler<DiscountHistory_GetAllQuery, Result<IEnumerable<DiscountHistoryDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountHistory_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<DiscountHistoryDto>>> Handle(DiscountHistory_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<DiscountHistoryDto> discount_histories = await _context.DiscountHistories.OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<DiscountHistoryDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

            return Result<IEnumerable<DiscountHistoryDto>>.Success(discount_histories);
        }
    }
}
