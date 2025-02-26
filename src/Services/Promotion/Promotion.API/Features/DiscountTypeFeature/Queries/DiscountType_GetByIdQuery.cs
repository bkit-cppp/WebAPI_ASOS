using Promotion.API.Features.DiscountTypeFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountTypeFeature.Queries
{
    public record DiscountType_GetByIdQuery(string Id) : IQuery<Result<DiscountTypeDto>>;
    public class DiscountType_GetByIdQueryHandler : IQueryHandler<DiscountType_GetByIdQuery, Result<DiscountTypeDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountType_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DiscountTypeDto>> Handle(DiscountType_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.DiscountTypes.Where(s => s.Id == request.Id)
                                  .ProjectTo<DiscountTypeDto>(_mapper.ConfigurationProvider)
                                  .FirstOrDefaultAsync();
            return Result<DiscountTypeDto>.Success(discount);
        }
    }
}
