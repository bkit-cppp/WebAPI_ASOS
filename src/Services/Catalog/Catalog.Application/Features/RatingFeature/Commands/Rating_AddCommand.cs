using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using Catalog.Application.Features.RatingFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.RatingFeature.Commands;
public record Rating_AddCommand(Rating RequestData) : ICommand<Result<RatingDto>>;

public class RatingAddCommandValidator : AbstractValidator<Rating_AddCommand>
{
    public RatingAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        RuleFor(command => command.RequestData.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}

public class Rating_AddCommandHandler : ICommandHandler<Rating_AddCommand, Result<RatingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public Rating_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<Result<RatingDto>> Handle(Rating_AddCommand request, CancellationToken cancellationToken)
    {
		var product = await _unitOfWork.Products.FindAsync(request.RequestData.ProductId!.Value);
		if (product == null)
		{
			return Result<RatingDto>.Failure("Product not found");
		}

        var rating = await _unitOfWork.Ratings.Queryable()
                                      .Where(s => s.UserId == request.RequestData.UserId &&
                                                  s.ProductId == product.Id)
                                      .FirstOrDefaultAsync();

        if(rating == null)
        {
            rating = new Rating()
			{
				ProductId = request.RequestData.ProductId,
				UserId = request.RequestData.UserId,
                Rate = request.RequestData.Rate
			};
			_unitOfWork.Ratings.Add(rating, request.RequestData.CreatedUser);
		}
        else
        {
			rating.Rate = request.RequestData.Rate;
			_unitOfWork.Ratings.Update(rating, request.RequestData.CreatedUser);
		}

        int rows = await _unitOfWork.CompleteAsync();
        if(rows > 0)
        {
            await _eventBus.PublishAsync(new CalcAverageRatingEvent()
            {
                ProductId = product.Id
            });
        }

        return Result<RatingDto>.Success(_mapper.Map<RatingDto>(rating));
    }

}