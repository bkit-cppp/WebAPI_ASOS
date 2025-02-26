using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetSimilarQuery(string slug,int take) : IQuery<Result<List<ProductOverviewDto>>>;
public class Product_GetSimilarQueryHandler : IQueryHandler<Product_GetSimilarQuery, Result<List<ProductOverviewDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Product_GetSimilarQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductOverviewDto>>> Handle(Product_GetSimilarQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.Queryable()
                                       .Where(s => s.Slug == request.slug)
                                       .Select(s => new Product()
                                       {
                                           Id = s.Id,
                                           CategoryGender = new CategoryGender()
                                           {
                                               Id = s.CategoryGender != null ? s.CategoryGender.Id : Guid.Empty,
                                               CategoryId = s.CategoryGender != null ? s.CategoryGender.CategoryId : Guid.Empty,
											   GenderId = s.CategoryGender != null ? s.CategoryGender.GenderId : Guid.Empty
										   },
                                           CategoryGenderId = s.CategoryGenderId,
                                           BrandId = s.BrandId ?? Guid.Empty,
                                       })
                                       .FirstOrDefaultAsync();

        if (product == null)
        {
            return Result<List<ProductOverviewDto>>.Empty();
		}

        var similarProducts = await _unitOfWork.Products.Queryable()
                                               .Where(s => s.CategoryGenderId == product.CategoryGenderId ||
                                                           s.BrandId == product.BrandId)
                                               .Take(request.take)
											   .ProjectTo<ProductOverviewDto>(_mapper.ConfigurationProvider)
											   .OrderByDescending(s => s.AverageRating)
											   .ToListAsync();

        return Result<List<ProductOverviewDto>>.Success(similarProducts);
    }
}
