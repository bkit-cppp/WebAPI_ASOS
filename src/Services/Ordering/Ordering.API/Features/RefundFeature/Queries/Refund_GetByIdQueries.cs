using Ordering.API.Features.RefundFeature.Dtos;

namespace Ordering.API.Features.RefundFeature.Queries
{
    public record Refund_GetByIdQueries(Guid Id) :IQuery<Result<RefundDto>>;
    public class Refund_GetByIdQueriesHandler : IQueryHandler<Refund_GetByIdQueries, Result<RefundDto>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public Refund_GetByIdQueriesHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;

        }
        public async Task<Result<RefundDto>> Handle(Refund_GetByIdQueries request, CancellationToken cancellationToken)
        {
            var refundDto = await _dataContext.Refunds.Where(s => s.Id == request.Id)
                               .ProjectTo<RefundDto>(_mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync();

            return Result<RefundDto>.Success(refundDto);
        }
    }

}
