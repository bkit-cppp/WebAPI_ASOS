namespace Catalog.Application.Features.WishListFeature.Commands;

public record WishList_DeleteByProduct(Guid product, Guid user) : ICommand<Result<bool>>;
public class WishList_DeleteByProductHandler : ICommandHandler<WishList_DeleteByProduct, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_DeleteByProductHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(WishList_DeleteByProduct request, CancellationToken cancellationToken)
    {
        var wishlist = await _unitOfWork.Wishlists.Queryable()
                            .Where(s => s.ProductId == request.product &&
                                        s.UserId == request.user)
                            .FirstOrDefaultAsync();


        if (wishlist == null)
            throw new ApplicationException("Wishlist not found");

        _unitOfWork.Wishlists.SoftDelete(wishlist, request.user);

        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true);
    }
}
