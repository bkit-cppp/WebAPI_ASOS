using Catalog.Application.Features.UserCommentFeature.Dto;

namespace Catalog.Application.Features.UserCommentFeature.Queries;

public record UserComment_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<UserCommentDto>>>;

public class UserComment_GetFilterQueryHandler : IQueryHandler<UserComment_GetFilterQuery, Result<IEnumerable<UserCommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserComment_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<UserCommentDto>>> Handle(UserComment_GetFilterQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.UserComments.Queryable().OrderedListQuery(orderCol, orderDir) // Assuming UserComments DbSet
                            .ProjectTo<UserCommentDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Fullname.Contains(request.RequestData.TextSearch) // Search by full name
                                    || s.Email.Contains(request.RequestData.TextSearch));  // Search by email
        }

        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<UserCommentDto>>.Success(await query.ToListAsync());
    }
}