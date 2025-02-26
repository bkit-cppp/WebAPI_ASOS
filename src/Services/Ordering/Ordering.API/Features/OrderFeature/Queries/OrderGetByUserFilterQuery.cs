using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Queries
{
    public record OrderGetByUserFilterQuery(Guid userId, FilterRequest RequestData) : IQuery<Result<IEnumerable<OrderDto>>>;
    public class OrderGetByUserFilterQueryQueryHandler : IQueryHandler<OrderGetByUserFilterQuery, Result<IEnumerable<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public OrderGetByUserFilterQueryQueryHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<Result<IEnumerable<OrderDto>>> Handle(OrderGetByUserFilterQuery request, CancellationToken cancellationToken)
        {

            var query = _dataContext.Orders
                .Where(o => o.UserId == request.userId) // Lọc theo UserId
                .OrderedListQuery(request.RequestData.OrderCol, request.RequestData.OrderDir)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.Type))
            {
                query = query.Where(s => s.StatusId == request.RequestData.Type);
            }

            if (request.RequestData.Skip.HasValue)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord.HasValue)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<OrderDto>>.Success(await query.ToListAsync());
        }
    }
    }


