using BuildingBlock.Core.Paging;
using Catalog.Application.Features.CommentFeature.Dto;

namespace Catalog.Application.Features.CommentFeature.Queries;


public record Comment_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<CommentDto>>>;
public class Comment_GetPaginationQueryHandler : IQueryHandler<Comment_GetPaginationQuery, Result<PaginatedList<CommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CommentDto>>> Handle(Comment_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.Comments.Queryable()
                               .OrderedListQuery(orderCol, orderDir)
                               .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                               .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Content.Contains(request.RequestData.TextSearch));
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<CommentDto>>.Success(paging);
    }
}