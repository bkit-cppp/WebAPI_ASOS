using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Features.ProductItemFeature.Dto;
using Catalog.Application.Features.VariationFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetDetailQuery(string slug, Guid user) : IQuery<Result<ProductDetailDto>>;
public class Product_GetDetailQueryHandler : IQueryHandler<Product_GetDetailQuery, Result<ProductDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public Product_GetDetailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<ProductDetailDto>> Handle(Product_GetDetailQuery request, CancellationToken cancellationToken)
    {
        var isLogin = request.user != Guid.Empty;

        var product = await _unitOfWork.Products.Queryable()
                                       .Where(s => s.Slug == request.slug)
                                       .Select(s => new ProductDetailDto()
                                       {
                                           Id = s.Id,
                                           Slug = s.Slug,
                                           Name = s.Name,
                                           Description = s.Description,
                                           AverageRating = s.AverageRating,
                                           SizeAndFit = s.SizeAndFit,
                                           Image = s.Image,
                                           OriginalPrice = s.OriginalPrice,
                                           SalePrice = s.SalePrice,
                                           IsSale = s.IsSale,
                                           Category = s.CategoryGender != null ? s.CategoryGender.Category.Name : "",
                                           Brand = s.Brand != null ? s.Brand.Name : "",
                                           Items = s.ProductItems != null ? s.ProductItems.Select(item => new ProductItemDetailDto()
                                           {
                                               Id = item.Id,
                                               AdditionalPrice = item.AdditionalPrice,
                                               Color = item.Color != null ? item.Color.Name : "",
                                               Images = item.Images != null ? item.Images.Select(s => s.Url).ToList() : new List<string>(),
                                               Variations = item.Variations != null ?
                                                            item.Variations
                                                            .Where(s => s.QtyDisplay > 0 && s.QtyInStock > 0)
                                                            .Select(va => new VariationDetailDto()
                                                            {
                                                                Id = va.Id,
                                                                QtyDisplay = va.QtyDisplay,
                                                                QtyInStock = va.QtyInStock,
                                                                Stock = va.Stock,
                                                                Size = va.Size != null ? va.Size.Name : ""
                                                            }).ToList() : new List<VariationDetailDto>()
                                           }).ToList() : new List<ProductItemDetailDto>(),
                                           Action = isLogin == true ? new ProductDetailAction()
                                           {
                                               AddWishlist = s.WishLists != null ? s.WishLists.Where(s => s.UserId == request.user).Count() > 0 : false,
                                               Rated = s.Ratings != null ? s.Ratings.Where(s => s.UserId == request.user).Count() > 0 : false,
                                               PointRated = s.Ratings != null ? s.Ratings.Where(s => s.UserId == request.user).Select(s => s.Rate).FirstOrDefault() : 0,
										   }: new ProductDetailAction()
									   })
                                       .FirstOrDefaultAsync();

        return Result<ProductDetailDto>.Success(product);
    }
}
