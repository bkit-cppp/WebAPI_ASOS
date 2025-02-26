using Promotion.API.Features.DiscountFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountFeature.Queries
{
    public record Discount_GetByCodeQuery(String Code) : IQuery<Result<DiscountDto>>;
    public class Discount_GetByCodeQueryHandler : IQueryHandler<Discount_GetByCodeQuery, Result<DiscountDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Discount_GetByCodeQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DiscountDto>> Handle(Discount_GetByCodeQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.Where(s => s.Code == request.Code)
                                  .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
                                  .FirstOrDefaultAsync();
            return Result<DiscountDto>.Success(discount);
        }
    }
}
