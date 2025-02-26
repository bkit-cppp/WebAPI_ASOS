using Promotion.API.Features.DiscountHistoryFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountHistoryFeature.Queries
{
    public record DiscountHistory_GetByIdQuery(Guid Id) : IQuery<Result<DiscountHistoryDto>>;
    public class DiscountHistory_GetByCodeQueryHandler : IQueryHandler<DiscountHistory_GetByIdQuery, Result<DiscountHistoryDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountHistory_GetByCodeQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DiscountHistoryDto>> Handle(DiscountHistory_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.DiscountHistories.Where(s => s.Id == request.Id)
                                  .ProjectTo<DiscountHistoryDto>(_mapper.ConfigurationProvider)
                                  .FirstOrDefaultAsync();
            return Result<DiscountHistoryDto>.Success(discount);
        }
    }

}
