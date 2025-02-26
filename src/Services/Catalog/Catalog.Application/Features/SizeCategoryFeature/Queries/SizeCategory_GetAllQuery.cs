using Catalog.Application.Features.SizeCategoryFeature.Dto;

namespace Catalog.Application.Features.SizeCategoryFeature.Queries;

// Nếu RequestData đơn giản thì dùng BaseRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class BaseRequest
// Ví dụ : Catalog.Application/Models/GenderModel/GenderGetAllRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock

public record SizeCategory_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<SizeCategoryDto>>>;
public class SizeCategory_GetAllQueryHandler : IQueryHandler<SizeCategory_GetAllQuery, Result<IEnumerable<SizeCategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<SizeCategoryDto>>> Handle(SizeCategory_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<SizeCategoryDto> Genders = await _unitOfWork.SizeCategories.Queryable()
                                               .OrderedListQuery(orderCol, orderDir)
                                               .ProjectTo<SizeCategoryDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();

        return Result<IEnumerable<SizeCategoryDto>>.Success(Genders);
    }
}
