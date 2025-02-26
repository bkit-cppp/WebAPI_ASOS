namespace Promotion.API.Features.DiscountFeature.Queries;

public record Discount_GetOptionByProductQuery(string ids) : IQuery<Result<List<SelectOption>>>;
public class Discount_GetOptionByProductQueryHandler : IQueryHandler<Discount_GetOptionByProductQuery, Result<List<SelectOption>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Discount_GetOptionByProductQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<SelectOption>>> Handle(Discount_GetOptionByProductQuery request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.Now;
        List<string> productIds = request.ids.Split(",").ToList();
		List<Guid> ids = productIds.Select(m => Guid.Parse(m)).ToList();

        var discountIds = await _context.DiscountProducts
                                .Where(s => s.ProductId != null && ids.Contains(s.ProductId.Value))
                                .Select(s => s.DiscountId)
                                .Distinct()
                                .ToListAsync();

		var discount = await _context.Discounts
                              .Where(s => discountIds.Contains(s.Id) &&
                                          s.EndDate > now && s.Available == true)
                              .Select(s => new SelectOption()
                              {
								  Label = $"{s.Code} - {s.Value}{(s.DiscountTypeId == DiscountTypeConstant.Percentage ? "%" : "£")}",
								  Value = s.Code
                              })
                              .ToListAsync();

        return Result<List<SelectOption>>.Success(discount);
    }
}
