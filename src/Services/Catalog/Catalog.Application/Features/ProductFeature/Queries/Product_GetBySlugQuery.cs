using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetBySlugQuery(string Slug) : IQuery<Result<ProductDto>>;
public class Product_GetBySlugQueryHandler : IQueryHandler<Product_GetBySlugQuery, Result<ProductDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Product_GetBySlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<ProductDto>> Handle(Product_GetBySlugQuery request, CancellationToken cancellationToken)
	{
		var products = await _unitOfWork.Products.Queryable()
									    .Where(p => p.Slug == request.Slug)
										.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
										.FirstOrDefaultAsync();

		return Result<ProductDto>.Success(products);
	}
}
