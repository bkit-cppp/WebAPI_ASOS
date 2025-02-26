using Catalog.Application.Features.UserCommentFeature.Dto;

namespace Catalog.Application.Features.UserCommentFeature.Queries;

public record UserComment_GetByIdQuery(Guid Id) : IQuery<Result<UserCommentDto>>;

public class UserComment_GetByIdQueryHandler : IQueryHandler<UserComment_GetByIdQuery, Result<UserCommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserComment_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<UserCommentDto>> Handle(UserComment_GetByIdQuery request, CancellationToken cancellationToken)
    {
        var userComment = await _unitOfWork.UserComments.Queryable()
                                      .Where(s => s.Id == request.Id)
                                      .ProjectTo<UserCommentDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<UserCommentDto>.Success(userComment);
    }
}
