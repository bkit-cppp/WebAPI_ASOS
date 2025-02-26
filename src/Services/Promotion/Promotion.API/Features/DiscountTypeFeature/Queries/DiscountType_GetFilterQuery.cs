using Promotion.API.Features.DiscountTypeFeature.Dto;
using Promotion.API.Data;
namespace Promotion.API.Features.DiscountTypeFeature.Queries
{
    public record DiscountType_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<DiscountTypeDto>>>;
    public class DiscountType_GetFilterQueryHandler : IQueryHandler<DiscountType_GetFilterQuery, Result<IEnumerable<DiscountTypeDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiscountType_GetFilterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DiscountTypeDto>>> Handle(DiscountType_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.DiscountTypes.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<DiscountTypeDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Name == request.RequestData.TextSearch);
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<DiscountTypeDto>>.Success(await query.ToListAsync());
        }
    }
}
