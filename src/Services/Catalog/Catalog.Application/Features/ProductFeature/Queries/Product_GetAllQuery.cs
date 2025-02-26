using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record  Product_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<ProductDto>>>;
public class Product_GetAllQueryHandler : IQueryHandler<Product_GetAllQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public Product_GetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<ProductDto>>> Handle(Product_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;
        IEnumerable<ProductDto> products = await _unitOfWork.Products.Queryable()
                                         // .Where(p => p.BrandId != null && p.CategoryId != null)
                                          .OrderedListQuery(orderCol, orderDir)
                                          .ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync();
        return Result<IEnumerable<ProductDto>>.Success(products);
       
    }
}
