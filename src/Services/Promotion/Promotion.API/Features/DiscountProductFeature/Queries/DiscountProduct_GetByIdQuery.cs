using Promotion.API.Features.DiscountProductFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountProductFeature.Queries
{
    public record DiscountProduct_GetByIdQuery(Guid Id) : IQuery<Result<DiscountProductDto>>;
    public class DiscountProduct_GetByIdQueryHandler : IQueryHandler<DiscountProduct_GetByIdQuery, Result<DiscountProductDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountProduct_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DiscountProductDto>> Handle(DiscountProduct_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.DiscountProducts.Where(s => s.Id == request.Id)
                                  .ProjectTo<DiscountProductDto>(_mapper.ConfigurationProvider)
                                  .FirstOrDefaultAsync();
            return Result<DiscountProductDto>.Success(discount);
        }
    }
}
