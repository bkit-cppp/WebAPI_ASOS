namespace Identity.API.Features.RoleFeature.Commands;

public record Role_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Role_DeleteCommandHandler : ICommandHandler<Role_DeleteCommand, Result<bool>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Role_DeleteCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Role_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		var query = await _context.Roles.Where(m => request.RequestData.Ids.Contains(m.Id)).ToListAsync();
		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}

		_context.Roles.UpdateRange(query);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}
