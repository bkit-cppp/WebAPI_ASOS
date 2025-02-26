using Catalog.Application.Features.WishListFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.WishListFeature.Commands;

public record WishList_AddCommand(WishList RequestData) : ICommand<Result<bool>>;

public class WishListAddCommandValidator : AbstractValidator<WishList_AddCommand>
{
    public WishListAddCommandValidator()
    {
        RuleFor(command => command.RequestData.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        RuleFor(command => command.RequestData.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}

public class WishList_AddCommandHandler : ICommandHandler<WishList_AddCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public WishList_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(WishList_AddCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.Queryable().FirstOrDefaultAsync(s => s.Id == request.RequestData.ProductId);
                        
        if (product == null)
        {
            throw new ApplicationException("Product not found");
        }

        var wishlist = await _unitOfWork.Wishlists.Queryable()
                             .FirstOrDefaultAsync(s => s.UserId ==  request.RequestData.UserId &&
                                                       s.ProductId == product.Id );

        if (wishlist == null)
        {
            wishlist = new WishList()
			{
				Id = request.RequestData.Id,
				ProductId = request.RequestData.ProductId,
				UserId = request.RequestData.UserId
			};
			_unitOfWork.Wishlists.Add(wishlist, request.RequestData.CreatedUser);
			await _unitOfWork.CompleteAsync();
		}

        return Result<bool>.Success(true);
    }
}