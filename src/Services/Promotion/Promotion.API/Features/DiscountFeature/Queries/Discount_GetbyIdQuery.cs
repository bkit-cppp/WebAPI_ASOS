using Promotion.API.Features.DiscountFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountFeature.Queries
{
    public record Discount_GetByIdQuery(Guid Id) : IQuery<Result<DiscountDto>>;
    public class Discount_GetByIdQueryHandler : IQueryHandler<Discount_GetByIdQuery, Result<DiscountDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Discount_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DiscountDto>> Handle(Discount_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.Where(s => s.Id == request.Id)
                                  .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
                                  .FirstOrDefaultAsync();
            return Result<DiscountDto>.Success(discount);
        }
    }
}
