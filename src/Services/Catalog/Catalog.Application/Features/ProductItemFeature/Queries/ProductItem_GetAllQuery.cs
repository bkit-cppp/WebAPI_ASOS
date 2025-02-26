
using Catalog.Application.Features.ProductItemFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.ProductItemFeature.Queries
{
   
    public record ProductItem_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<ProductItemDto>>>;
    public class ProductItem_GetAllQueryHandler : IQueryHandler<ProductItem_GetAllQuery, Result<IEnumerable<ProductItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductItem_GetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductItemDto>>> Handle(ProductItem_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;
            IEnumerable<ProductItemDto> productItems = await _unitOfWork.ProductItems.Queryable()
                                              .Where(p => p.ColorId!=null&& p.ProductId!=null)
                                             .OrderedListQuery(orderCol, orderDir)
                                             .ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Result<IEnumerable<ProductItemDto>>.Success(productItems);
        }
    }
}
