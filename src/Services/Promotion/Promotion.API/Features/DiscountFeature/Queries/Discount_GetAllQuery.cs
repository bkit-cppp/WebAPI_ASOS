using Promotion.API.Features.DiscountFeature.Dto;
using Promotion.API.Data;

namespace Promotion.API.Features.DiscountFeature.Queries
{
    public record Discount_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<DiscountDto>>>;
    public class Discount_GetAllQueryHandler : IQueryHandler<Discount_GetAllQuery, Result<IEnumerable<DiscountDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Discount_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<DiscountDto>>> Handle(Discount_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<DiscountDto> discounts = await _context.Discounts.OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

            return Result<IEnumerable<DiscountDto>>.Success(discounts);
        }
    }
}
