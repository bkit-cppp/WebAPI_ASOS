using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetNewestQuery(BaseRequest Request) : IQuery<List<ProductDto>>;
public class Product_GetNewestQueryHandler : IQueryHandler<Product_GetNewestQuery, List<ProductDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Product_GetNewestQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<List<ProductDto>> Handle(Product_GetNewestQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.Request.OrderCol;
		var orderDir = request.Request.OrderDir;
		var productNewest = await _unitOfWork.Products.GetNewestProductAsync();

		return productNewest.Select(p => new ProductDto
		{
			Id = p.Id,
			Name = p.Name,
			Description = p.Description,
			Slug = p.Slug,
			SizeAndFit = p.SizeAndFit,
			AverageRating = p.AverageRating
		}).ToList();

	}

}
