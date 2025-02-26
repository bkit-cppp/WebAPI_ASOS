using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.UserCommentFeature.Commands
{
    public record UserComment_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;

    public class UserComment_DeleteCommandHandler : ICommandHandler<UserComment_DeleteCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserComment_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UserComment_DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.RequestData.Ids == null)
                throw new ApplicationException("Ids not found");

            IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
            var userComments = await _unitOfWork.UserComments.FindByIds(ids, true); // Assuming you have a UserComments DbSet

            _unitOfWork.UserComments.SoftDeleteRange(userComments, request.RequestData.ModifiedUser);

            await _unitOfWork.CompleteAsync();
            return Result<bool>.Success(true);
        }
    }
}