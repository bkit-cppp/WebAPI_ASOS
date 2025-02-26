using Catalog.Application.Features.SizeCategoryFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public record SizeCategory_GetBySizeIdQuery(Guid SizeCategoryId) : IQuery<Result<SizeCategoryDto>>;
public class Size_GetBySizeIdQueryHandler : IQueryHandler<SizeCategory_GetBySizeIdQuery, Result<SizeCategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Size_GetBySizeIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SizeCategoryDto>> Handle(SizeCategory_GetBySizeIdQuery request, CancellationToken cancellationToken)
    {

        var SizeCategory = await _unitOfWork.Sizes.Queryable()
                                      .Where(s => s.Id == request.SizeCategoryId)
                                      .ProjectTo<SizeCategoryDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<SizeCategoryDto>.Success(SizeCategory);
    }
}
