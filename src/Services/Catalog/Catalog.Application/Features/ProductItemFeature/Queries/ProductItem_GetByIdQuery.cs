
using Catalog.Application.Features.ProductItemFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.ProductItemFeature.Queries
{
    public record ProductItem_GetByIdQuery(Guid Id) : IQuery<Result<ProductItemDto>>;
    public class ProductItem_GetByIdQueryHandler : IQueryHandler<ProductItem_GetByIdQuery, Result<ProductItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductItem_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<ProductItemDto>> Handle(ProductItem_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var Id = request.Id;
            var product = await _unitOfWork.ProductItems.Queryable()
                                           .Where(s => s.Id == Id && s.ColorId != null && s.ProductId != null)
                                           .ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider)
                                           .SingleOrDefaultAsync();

            return Result<ProductItemDto>.Success(product);
        }
    }
}
