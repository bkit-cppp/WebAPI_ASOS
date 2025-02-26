namespace Catalog.Application.Features.WishListFeature.Commands;

public record WishList_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class WishList_DeleteCommandHandler : ICommandHandler<WishList_DeleteCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(WishList_DeleteCommand request, CancellationToken cancellationToken)
    {
        if (request.RequestData.Ids == null)
            throw new ApplicationException("Ids not found");

        IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
        var wishlists = await _unitOfWork.Wishlists.FindByIds(ids, true);

        _unitOfWork.Wishlists.SoftDeleteRange(wishlists, request.RequestData.ModifiedUser);

        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true);
    }
}
