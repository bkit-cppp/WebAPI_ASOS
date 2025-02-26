

using Catalog.Application.Features.VariationFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Queries
{
    
    public record Variation_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<VariationDto>>>;
    public class Variation_GetFilterQueryHandler : IQueryHandler<Variation_GetFilterQuery, Result<IEnumerable<VariationDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public Variation_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<VariationDto>>> Handle(Variation_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;
            var variations = _unitOfWork.Variations.Queryable()
                                                   .Where(s => s.SizeId != null && s.ProductItemId != null)
                                                   .OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<VariationDto>(_mapper.ConfigurationProvider)
                                                  .AsNoTracking();
            if (!string.IsNullOrEmpty(request.RequestData.TextSearch) &&
            int.TryParse(request.RequestData.TextSearch, out int searchQty))
            {
                variations = variations.Where(s => s.QtyDisplay == searchQty);
            }
            if (request.RequestData.Skip != null)
            {
                variations = variations.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                variations = variations.Take(request.RequestData.TotalRecord.Value);
            }
            return Result<IEnumerable<VariationDto>>.Success(variations);
        }
    }
}
