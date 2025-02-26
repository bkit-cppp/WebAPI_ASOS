
namespace Identity.API.Features.PointHistoryFeature.Commands;

	public record PointHistory_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class PointHistory_DeleteCommandHandler : ICommandHandler<PointHistory_DeleteCommand, Result<bool>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PointHistory_DeleteCommandHandler(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Result<bool>> Handle(PointHistory_DeleteCommand request, CancellationToken cancellationToken)
    {
        if (request.RequestData.Ids == null)
            throw new ApplicationException("Ids not found");

        List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
        var query = await _context.PointHistories.Where(m => ids.Contains(m.Id)).ToListAsync();
        if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

        foreach (var item in query)
        {
            item.DeleteFlag = true;
            item.ModifiedDate = DateTime.Now;
            item.ModifiedUser = request.RequestData.ModifiedUser;
        }

        _context.PointHistories.UpdateRange(query);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
