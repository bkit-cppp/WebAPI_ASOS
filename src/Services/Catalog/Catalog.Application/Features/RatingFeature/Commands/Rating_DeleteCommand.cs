namespace Catalog.Application.Features.RatingFeature.Commands;

public record Rating_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Rating_DeleteCommandHandler : ICommandHandler<Rating_DeleteCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Rating_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(Rating_DeleteCommand request, CancellationToken cancellationToken)
    {
        if (request.RequestData.Ids == null)
            throw new ApplicationException("Ids not found");

        IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
        var Ratings = await _unitOfWork.Ratings.FindByIds(ids, true);

        _unitOfWork.Ratings.SoftDeleteRange(Ratings, request.RequestData.ModifiedUser);

        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true);
    }
}
