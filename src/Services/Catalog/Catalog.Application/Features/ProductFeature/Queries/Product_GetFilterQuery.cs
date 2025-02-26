using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries
{
    public record Product_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<ProductDto>>>;
    public class Product_GetFilterQueryHandler : IQueryHandler<Product_GetFilterQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public Product_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(Product_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var products = _unitOfWork.Products.Queryable()
                                      .OrderedListQuery(orderCol, orderDir)
                                      .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                      .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                products = products.Where(s => s.Name.Contains(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                products = products.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                products = products.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<ProductDto>>.Success(await products.ToListAsync());
        }
    }
}
