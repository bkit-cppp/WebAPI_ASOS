using Catalog.Application.Features.WishListFeature.Dto;

namespace Catalog.Application.Features.WishListFeature.Queries;

public record WishList_GetByIdQuery(Guid Id) : IQuery<Result<WishListDto>>;
public class WishList_GetByIdQueryHandler : IQueryHandler<WishList_GetByIdQuery, Result<WishListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<WishListDto>> Handle(WishList_GetByIdQuery request, CancellationToken cancellationToken)
    {

        var wishlists = await _unitOfWork.Wishlists.Queryable()
                                      .Where(s => s.Id == request.Id)
                                      .ProjectTo<WishListDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<WishListDto>.Success(wishlists);
    }
}