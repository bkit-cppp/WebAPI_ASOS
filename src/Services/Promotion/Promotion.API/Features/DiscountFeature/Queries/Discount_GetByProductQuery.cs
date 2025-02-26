using Promotion.API.Features.DiscountFeature.Dto;
namespace Promotion.API.Features.DiscountFeature.Queries;

public record Discount_GetByProductQuery(string ids) : IQuery<Result<DiscountDto>>;
public class Discount_GetByProductQueryHandler : IQueryHandler<Discount_GetByProductQuery, Result<DiscountDto>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Discount_GetByProductQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<DiscountDto>> Handle(Discount_GetByProductQuery request, CancellationToken cancellationToken)
    {
        List<string> productIds = request.ids.Split(",").ToList();
		List<Guid> ids = productIds.Select(m => Guid.Parse(m)).ToList();

        var discountIds = await _context.DiscountProducts
                                .Where(s => s.ProductId != null && ids.Contains(s.ProductId.Value))
                                .Select(s => s.DiscountId)
                                .ToListAsync();

		var discount = await _context.Discounts
                              .Where(s => discountIds.Contains(s.Id))
                              .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync();

        return Result<DiscountDto>.Success(discount);
    }
}
