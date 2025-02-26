using Promotion.API.Features.DiscountProductFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountProductFeature.Queries
{
        public record DiscountProduct_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<DiscountProductDto>>>;
        public class DiscountProduct_GetAllQueryHandler : IQueryHandler<DiscountProduct_GetAllQuery, Result<IEnumerable<DiscountProductDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public DiscountProduct_GetAllQueryHandler(IMapper mapper, DataContext context)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<IEnumerable<DiscountProductDto>>> Handle(DiscountProduct_GetAllQuery request, CancellationToken cancellationToken)
            {
                var orderCol = request.RequestData.OrderCol;
                var orderDir = request.RequestData.OrderDir;

                IEnumerable<DiscountProductDto> discounts = await _context.DiscountProducts.OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<DiscountProductDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync();

                return Result<IEnumerable<DiscountProductDto>>.Success(discounts);
            }
        }
}
