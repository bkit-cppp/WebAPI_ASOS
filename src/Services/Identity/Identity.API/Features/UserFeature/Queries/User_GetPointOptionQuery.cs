namespace Identity.API.Features.UserFeature.Queries;

public record User_GetPointOptionQuery(Guid id) : IQuery<Result<IEnumerable<SelectOption>>>;
public class User_GetPointOptionQueryHandler : IQueryHandler<User_GetPointOptionQuery, Result<IEnumerable<SelectOption>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public User_GetPointOptionQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<SelectOption>>> Handle(User_GetPointOptionQuery request, CancellationToken cancellationToken)
	{
		int point = await _context.Users.Where(s => s.Id == request.id)
								  .Select(s => s.Point).FirstOrDefaultAsync();

		List<SelectOption> options = new List<SelectOption>();
		if (point >= 5)
		{
			options.Add(new SelectOption
			{
				Value = "5",
				Label = "Use 5 point"
			});
		}
		if (point >= 10)
		{
			options.Add(new SelectOption
			{
				Value = "10",
				Label = "Use 10 point"
			});
		}
		if (point >= 20)
		{
			options.Add(new SelectOption
			{
				Value = "20",
				Label = "Use 20 point"
			});
		}
		if (point >= 50)
		{
			options.Add(new SelectOption
			{
				Value = "50",
				Label = "Use 50 point"
			});
		}
		if (point >= 100)
		{
			options.Add(new SelectOption
			{
				Value = "100",
				Label = "Use 100 point"
			});
		}

		return Result<IEnumerable<SelectOption>>.Success(options);
	}
}