namespace Catalog.Infrastructure.Repository;

public class UserCommentRepository : GenericRepository<UserComment, Guid>, IUserCommentRepository
{
	public UserCommentRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
