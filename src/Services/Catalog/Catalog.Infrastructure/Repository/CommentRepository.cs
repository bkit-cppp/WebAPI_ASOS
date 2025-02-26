namespace Catalog.Infrastructure.Repository;

public class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
{
	public CommentRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
