using BuildingBlock.Core.Paging;
using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Models.ProductModel;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetPaginationOverviewQuery(ProductOverviewPaginationRequest RequestData) : IQuery<Result<PaginatedList<ProductOverviewDto>>>;
public class Product_GetPaginationOverviewHandler : IQueryHandler<Product_GetPaginationOverviewQuery, Result<PaginatedList<ProductOverviewDto>>>
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	public Product_GetPaginationOverviewHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<PaginatedList<ProductOverviewDto>>> Handle(Product_GetPaginationOverviewQuery request, CancellationToken cancellationToken)
	{
		string all = "all";

		var query = _unitOfWork.Products.Queryable().AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			string text = $"%{request.RequestData.TextSearch}%";
			query = query.Where(s => EF.Functions.Like(s.Name, text));
		}

		if (request.RequestData.Sale != null)
		{
			query = query.Where(s => s.IsSale == request.RequestData.Sale);
		}

		if (!string.IsNullOrEmpty(request.RequestData.Price))
		{
			string price = request.RequestData.Price;

			if (price == "under-100")
			{
				query = query.Where(s => s.IsSale == true ? (s.SalePrice <= 100) : (s.OriginalPrice <= 100));
			}
			else if (price == "100-to-500")
			{
				query = query.Where(s => s.IsSale == true ? 
										(s.SalePrice >= 100 && s.SalePrice <= 500) : 
										(s.OriginalPrice >= 100 && s.OriginalPrice <= 500));
			}
			else if (price == "500-1000")
			{
				query = query.Where(s => s.IsSale == true ?
										(s.SalePrice >= 500 && s.SalePrice <= 1000) :
										(s.OriginalPrice >= 500 && s.OriginalPrice <= 1000));
			}
			else if (price == "over-1000")
			{
				query = query.Where(s => s.IsSale == true ? (s.SalePrice >= 1000) : (s.OriginalPrice >= 1000));
			}
		}

		if (!string.IsNullOrEmpty(request.RequestData.Sort))
		{
			string sort = request.RequestData.Sort;

			if (sort == "revelance")
			{
				query = query.OrderByDescending(s => s.CreatedDate);
			}
			else if (sort == "price-high-to-low")
			{
				query = query.OrderByDescending(s => s.IsSale == true ? s.SalePrice : s.OriginalPrice);
			}
			else if (sort == "price-low-to-high")
			{
				query = query.OrderBy(s => s.IsSale == true ? s.SalePrice : s.OriginalPrice);
			}
			else if (sort == "highest-rating")
			{
				query = query.OrderByDescending(s => s.AverageRating);
			}
		}

		if (!string.IsNullOrEmpty(request.RequestData.Gender) && request.RequestData.Gender != all)
		{
			var id = await _unitOfWork.Genders.Queryable()
							.Where(s => s.Slug == request.RequestData.Gender)
							.Select(s => s.Id).FirstOrDefaultAsync();

			query = query.Where(s => s.CategoryGender != null && s.CategoryGender.GenderId == id);
		}

		if (!string.IsNullOrEmpty(request.RequestData.Category) && request.RequestData.Category != all)
		{
			var id = await _unitOfWork.Categories.Queryable()
							.Where(s => s.Slug == request.RequestData.Category)
							.Select(s => s.Id).FirstOrDefaultAsync();

			query = query.Where(s => s.CategoryGender != null && s.CategoryGender.CategoryId == id);
		}

		if (!string.IsNullOrEmpty(request.RequestData.Brand) && request.RequestData.Brand != all)
		{
			var id = await _unitOfWork.Brands.Queryable()
							.Where(s => s.Slug == request.RequestData.Brand)
							.Select(s => s.Id).FirstOrDefaultAsync();

			query = query.Where(s => s.BrandId == id);
		}

		var paging = await query.ProjectTo<ProductOverviewDto>(_mapper.ConfigurationProvider)
								.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		
		return Result<PaginatedList<ProductOverviewDto>>.Success(paging);
	}
}
