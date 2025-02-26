using Catalog.Application.Features.UserCommentFeature.Dto;

namespace Catalog.Application.Features.UserCommentFeature.Queries;
public record UserComment_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<UserCommentDto>>>;
public class UserComment_GetAllQueryHandler : IQueryHandler<UserComment_GetAllQuery, Result<IEnumerable<UserCommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserComment_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<UserCommentDto>>> Handle(UserComment_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<UserCommentDto> Comments = await _unitOfWork.UserComments.Queryable()
                                               .OrderedListQuery(orderCol, orderDir)
                                               .ProjectTo<UserCommentDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();
        return Result<IEnumerable<UserCommentDto>>.Success(Comments);
    }
}
