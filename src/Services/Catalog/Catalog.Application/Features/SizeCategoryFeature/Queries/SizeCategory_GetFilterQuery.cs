using Catalog.Application.Features.SizeCategoryFeature.Dto;
namespace Catalog.Application.Features.SizeCategoryFeature.Queries;

// Nếu RequestData đơn giản thì dùng FilterRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class FilterRequest
// Ví dụ : Catalog.Application/Models/SizeCategoryModel/SizeCategoryFilterRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record SizeCategory_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<SizeCategoryDto>>>;
public class SizeCategory_GetFilterQueryHandler : IQueryHandler<SizeCategory_GetFilterQuery, Result<IEnumerable<SizeCategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<SizeCategoryDto>>> Handle(SizeCategory_GetFilterQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.SizeCategories.Queryable().OrderedListQuery(orderCol, orderDir)
                            .ProjectTo<SizeCategoryDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Id.ToString().Contains(request.RequestData.TextSearch));
        }

        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<SizeCategoryDto>>.Success(await query.ToListAsync());
    }
}