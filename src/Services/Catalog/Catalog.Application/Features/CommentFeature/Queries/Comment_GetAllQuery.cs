using Catalog.Application.Features.CommentFeature.Dto;

namespace Catalog.Application.Features.CommentFeature.Queries;
public record Comment_GetAllQuery(Guid id,BaseRequest RequestData) : IQuery<Result<IEnumerable<CommentDto>>>;
public class Comment_GetAllQueryHandler : IQueryHandler<Comment_GetAllQuery, Result<IEnumerable<CommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<CommentDto>>> Handle(Comment_GetAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CommentDto> Comments = await _unitOfWork.Comments.Queryable()
                                               .OrderByDescending(s => s.CreatedDate)
                                               .Where(s => s.ParentId == null && s.ProductId == request.id)
                                               .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();
        return Result<IEnumerable<CommentDto>>.Success(Comments);
    }
}
