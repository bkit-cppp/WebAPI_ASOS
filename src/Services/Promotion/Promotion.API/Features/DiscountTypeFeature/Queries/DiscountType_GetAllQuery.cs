using Promotion.API.Features.DiscountTypeFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountTypeFeature.Queries
{
        public record DiscountType_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<DiscountTypeDto>>>;
        public class DiscountType_GetAllQueryHandler : IQueryHandler<DiscountType_GetAllQuery, Result<IEnumerable<DiscountTypeDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public DiscountType_GetAllQueryHandler(IMapper mapper, DataContext context)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<IEnumerable<DiscountTypeDto>>> Handle(DiscountType_GetAllQuery request, CancellationToken cancellationToken)
            {
                var orderCol = request.RequestData.OrderCol;
                var orderDir = request.RequestData.OrderDir;

                IEnumerable<DiscountTypeDto> discounts = await _context.DiscountTypes.OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<DiscountTypeDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync();

                return Result<IEnumerable<DiscountTypeDto>>.Success(discounts);
            }
        }

}
