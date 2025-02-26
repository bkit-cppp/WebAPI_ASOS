using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetByIdQuery(Guid Id) : IQuery<Result<ProductDto>>;
public class Product_GetByIdQueryHandler : IQueryHandler<Product_GetByIdQuery, Result<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Product_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<Result<ProductDto>> Handle(Product_GetByIdQuery request, CancellationToken cancellationToken)
    {
        var Id = request.Id;
        var product = await _unitOfWork.Products.Queryable()
                                       .Where(s => s.Id == Id)
                                       .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                       .SingleOrDefaultAsync();

        return Result<ProductDto>.Success(product);
    }
}
