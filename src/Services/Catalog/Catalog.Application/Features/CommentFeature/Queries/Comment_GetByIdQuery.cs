using Catalog.Application.Features.CommentFeature.Dto;

namespace Catalog.Application.Features.CommentFeature.Queries;

public record Comment_GetByIdQuery(Guid Id) : IQuery<Result<CommentDto>>;

public class Comment_GetByIdQueryHandler : IQueryHandler<Comment_GetByIdQuery, Result<CommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<CommentDto>> Handle(Comment_GetByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.Queryable()
                                      .Where(s => s.Id == request.Id)
                                      .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<CommentDto>.Success(comment);
    }
}
