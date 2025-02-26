using Catalog.Application.Features.SizeCategoryFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public record SizeCategory_GetByCategoryIdQuery(Guid CategoryId) : IQuery<Result<SizeCategoryDto>>;
public class Size_GetByCategoryIdQueryHandler : IQueryHandler<SizeCategory_GetByCategoryIdQuery, Result<SizeCategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Size_GetByCategoryIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SizeCategoryDto>> Handle(SizeCategory_GetByCategoryIdQuery request, CancellationToken cancellationToken)
    {

        var CategoryId = await _unitOfWork.Sizes.Queryable()
                                      .Where(s => s.Id == request.CategoryId)
                                      .ProjectTo<SizeCategoryDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<SizeCategoryDto>.Success(CategoryId);
    }
}
