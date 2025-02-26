namespace Catalog.Application.Features.SizeCategoryFeature.Commands;

public record SizeCategory_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class SizeCategory_DeleteCommandHandler : ICommandHandler<SizeCategory_DeleteCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(SizeCategory_DeleteCommand request, CancellationToken cancellationToken)
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