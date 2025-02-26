using Catalog.Application.Features.RatingFeature.Dto;

namespace Catalog.Application.Features.RatingFeature.Queries;

public record Rating_GetByIdQuery(Guid Id) : IQuery<Result<RatingDto>>;
public class Rating_GetByIdQueryHandler : IQueryHandler<Rating_GetByIdQuery, Result<RatingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Rating_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RatingDto>> Handle(Rating_GetByIdQuery request, CancellationToken cancellationToken)
    {

        var rating = await _unitOfWork.Ratings.Queryable()
                                      .Where(s => s.Id == request.Id)
                                      .ProjectTo<RatingDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<RatingDto>.Success(rating);
    }
}