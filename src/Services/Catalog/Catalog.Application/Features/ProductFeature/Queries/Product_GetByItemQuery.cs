using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetByItemQuery(Guid id) : IQuery<Result<ProductDto>>;
public class Product_GetByItemQueryHandler : IQueryHandler<Product_GetByItemQuery, Result<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Product_GetByItemQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<Result<ProductDto>> Handle(Product_GetByItemQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductItems.Queryable().Include(s => s.Product)
                                       .Where(s => s.Id == request.id && s.Product != null)
                                       .Select(s => new ProductDto()
                                       {
                                           Id = s.Product!.Id,
                                           Name = s.Product.Name
                                       })
                                       .FirstOrDefaultAsync();

        return Result<ProductDto>.Success(product);
    }
}
