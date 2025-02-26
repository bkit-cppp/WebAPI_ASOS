using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Commands
{

    public record Variation_DeleteCommand(DeleteRequest DeleteRequest) : ICommand<Result<bool>>;
    public class Variation_DeleteCommandHandler : ICommandHandler<Variation_DeleteCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        public Variation_DeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitofWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(Variation_DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.DeleteRequest.Ids == null)
                throw new ApplicationException("Ids not found");

            IEnumerable<Guid> ids = request.DeleteRequest.Ids.Select(m => Guid.Parse(m)).ToList();
            var variations = await _unitofWork.Variations.FindByIds(ids, true);

            _unitofWork.Variations.SoftDeleteRange(variations, request.DeleteRequest.ModifiedUser);

            await _unitofWork.CompleteAsync();
            return Result<bool>.Success(true);
        }
    }
}
