using Catalog.Application.Features.ColorFeature.Dto;

namespace Catalog.Application.Features.ColorFeature.Commands;

public record Color_UpdateCommand(Color RequestData) : ICommand<Result<ColorDto>>;
public class Color_UpdateCommandHandler : ICommandHandler<Color_UpdateCommand, Result<ColorDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Color_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<ColorDto>> Handle(Color_UpdateCommand request, CancellationToken cancellationToken)
	{
		var color = await _unitOfWork.Colors.FindAsync(request.RequestData.Id, true);

		if (color!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Colors.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != color.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			color.Slug = request.RequestData.Slug;
		}

		color.Name = request.RequestData.Name;
		color.Description = request.RequestData.Description;

		_unitOfWork.Colors.Update(color, request.RequestData.ModifiedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Color);
		}

		return Result<ColorDto>.Success(_mapper.Map<ColorDto>(color));
	}
}

