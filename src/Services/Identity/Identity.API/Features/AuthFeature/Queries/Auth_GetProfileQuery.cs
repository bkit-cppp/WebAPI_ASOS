using Identity.API.Features.AuthFeature.Dto;

namespace Identity.API.Features.AuthFeature.Queries;

public record Auth_GetProfileQuery(Guid Id) : IQuery<Result<ProfileDto>>;
public class Auth_GetProfileQueryHandler : IQueryHandler<Auth_GetProfileQuery, Result<ProfileDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Auth_GetProfileQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<ProfileDto>> Handle(Auth_GetProfileQuery request, CancellationToken cancellationToken)
	{
		var users = await _context.Users.Where(s => s.Id == request.Id)
								  .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider)
								  .FirstOrDefaultAsync();
		return Result<ProfileDto>.Success(users);
	}
}