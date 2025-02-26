
using Catalog.Application.Features.ProductItemFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.ProductItemFeature.Queries
{
    
    public record ProductItem_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<ProductItemDto>>>;
    public class ProductItem_GetFilterQueryHandler : IQueryHandler<ProductItem_GetFilterQuery, Result<IEnumerable<ProductItemDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductItem_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<ProductItemDto>>> Handle(ProductItem_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;
            var productsItem = _unitOfWork.ProductItems.Queryable()
                                                    .Where(s => s.ColorId != null && s.ProductId != null)
                                                   .OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider)
                                                  .AsNoTracking();
          
            if (request.RequestData.Skip != null)
            {
                productsItem = productsItem.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                productsItem = productsItem.Take(request.RequestData.TotalRecord.Value);
            }
            return Result<IEnumerable<ProductItemDto>>.Success(productsItem);
        }
    }
}
