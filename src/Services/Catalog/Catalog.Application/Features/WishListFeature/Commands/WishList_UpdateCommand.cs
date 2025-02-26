using Catalog.Application.Features.WishListFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.WishListFeature.Commands;

public record WishList_UpdateCommand(WishList RequestData) : ICommand<Result<WishListDto>>;

public class WishListUpdateCommandValidator : AbstractValidator<WishList_UpdateCommand>
{
    public WishListUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        RuleFor(command => command.RequestData.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}

public class WishList_UpdateCommandHandler : ICommandHandler<WishList_UpdateCommand, Result<WishListDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<WishListDto>> Handle(WishList_UpdateCommand request, CancellationToken cancellationToken)
    {

        var wishlist = await _unitOfWork.Wishlists.FindAsync(request.RequestData.Id, true);

        wishlist!.Product = request.RequestData.Product;
        wishlist.ModifiedDate = DateTime.Now;
        wishlist.ModifiedUser = request.RequestData.ModifiedUser;
        _unitOfWork.Wishlists.Update(wishlist, request.RequestData.ModifiedUser);
        await _unitOfWork.CompleteAsync();

        return Result<WishListDto>.Success(_mapper.Map<WishListDto>(wishlist));
    }
}