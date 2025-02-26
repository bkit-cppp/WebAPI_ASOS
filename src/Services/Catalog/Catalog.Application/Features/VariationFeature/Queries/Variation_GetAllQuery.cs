
using Catalog.Application.Features.VariationFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Queries
{
    public record Variation_GetAllQuery(BaseRequest RequestData):IQuery<Result<IEnumerable<VariationDto>>>;
    public class Color_GetAllHandler : IQueryHandler<Variation_GetAllQuery, Result<IEnumerable<VariationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Color_GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<VariationDto>>> Handle(Variation_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var variations = await _unitOfWork.Variations.Queryable().OrderedListQuery(orderCol, orderDir)
                                          .Where(s => s.SizeId != null || s.ProductItemId!=null)
                                          .ProjectTo<VariationDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync();

            return Result<IEnumerable<VariationDto>>.Success(variations);
        }
    }
}
