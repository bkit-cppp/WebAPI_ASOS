using Catalog.Application.Features.WishListFeature.Dto;

namespace Catalog.Application.Features.WishListFeature.Queries;
public record WishList_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<WishListDto>>>;
public class WishList_GetAllQueryHandler : IQueryHandler<WishList_GetAllQuery, Result<IEnumerable<WishListDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<WishListDto>>> Handle(WishList_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<WishListDto> Genders = await _unitOfWork.Wishlists.Queryable()
                                               .OrderedListQuery(orderCol, orderDir)
                                               .ProjectTo<WishListDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();

        return Result<IEnumerable<WishListDto>>.Success(Genders);
    }
}