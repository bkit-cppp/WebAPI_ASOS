using Catalog.Application.Features.SizeCategoryFeature.Dto;

namespace Catalog.Application.Features.SizeCategoryFeature.Queries;

public record SizeCategory_GetByIdQuery(Guid Id) : IQuery<Result<SizeCategoryDto>>;
public class SizeCategory_GetByIdQueryHandler : IQueryHandler<SizeCategory_GetByIdQuery, Result<SizeCategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SizeCategoryDto>> Handle(SizeCategory_GetByIdQuery request, CancellationToken cancellationToken)
    {

        var SizeCategorys = await _unitOfWork.SizeCategories.Queryable()
                                      .Where(s => s.Id == request.Id)
                                      .ProjectTo<SizeCategoryDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<SizeCategoryDto>.Success(SizeCategorys);
    }
}