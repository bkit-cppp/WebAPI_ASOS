using BuildingBlock.Utilities;
using Catalog.Application.Features.CommentFeature.Dto;
using Catalog.Application.Models.CommentModel;

namespace Catalog.Application.Features.CommentFeature.Queries;

public record Comment_GetFilterQuery(CommentFilterRequest RequestData) : IQuery<Result<IEnumerable<CommentDto>>>;
public class Comment_GetFilterQueryHandler : IQueryHandler<Comment_GetFilterQuery, Result<IEnumerable<CommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CommentDto>>> Handle(Comment_GetFilterQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Comments.Queryable().OrderByDescending(s => s.CreatedDate)
                            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Content.Contains(request.RequestData.TextSearch));
        }

        if (!StringHelper.GuidIsNull(request.RequestData.ProductId))
        {
			query = query.Where(s => s.ProductId == request.RequestData.ProductId);
		}

		if (!StringHelper.GuidIsNull(request.RequestData.ParentId))
		{
			query = query.Where(s => s.ParentId == request.RequestData.ParentId);
		}
        else
        {
            query = query.Where(s => s.ParentId == null);
        }

		if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<CommentDto>>.Success(await query.ToListAsync());
    }
}