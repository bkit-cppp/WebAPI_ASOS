
using Catalog.Application.Features.VariationFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Queries
{
    public record Variation_GetByIdQuery(Guid Id) : IQuery<Result<VariationDto>>;
    public class Variation_GetByIdQueryHandler : IQueryHandler<Variation_GetByIdQuery, Result<VariationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Variation_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<VariationDto>> Handle(Variation_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var Id = request.Id;
            var product = await _unitOfWork.Variations.Queryable()
                                           .Where(s => s.Id == Id || s.ProductItemId!= null || s.SizeId != null)
                                           .ProjectTo<VariationDto>(_mapper.ConfigurationProvider)
                                           .SingleOrDefaultAsync();

            return Result<VariationDto>.Success(product);
        }
    }
}
