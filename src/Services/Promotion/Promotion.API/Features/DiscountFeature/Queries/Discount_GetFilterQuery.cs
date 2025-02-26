using Promotion.API.Features.DiscountFeature.Dto;
namespace Promotion.API.Features.DiscountFeature.Queries
{
    public record Discount_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<DiscountDto>>>;
    public class Discount_GetFilterQueryHandler : IQueryHandler<Discount_GetFilterQuery, Result<IEnumerable<DiscountDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Discount_GetFilterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DiscountDto>>> Handle(Discount_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Discounts.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

			if (!string.IsNullOrEmpty(request.RequestData.Type))
			{
				query = query.Where(s => s.DiscountTypeId == request.RequestData.Type);
			}

			if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Code.Contains(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<DiscountDto>>.Success(await query.ToListAsync());
        }
    }
}
