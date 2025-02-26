using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Catalog.Application.Features.ProductFeature.Consumers;

public class CalcAverageRatingConsumer : IConsumer<CalcAverageRatingEvent>
{
	private readonly IUnitOfWork _unitOfWork;
	public CalcAverageRatingConsumer(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task Consume(ConsumeContext<CalcAverageRatingEvent> consumer)
	{
		var product = await _unitOfWork.Products.Queryable().FirstOrDefaultAsync(s => s.Id == consumer.Message.ProductId);
		if(product != null)
		{
			double averageRating = await _unitOfWork.Ratings.Queryable()
												 .Where(s => s.ProductId == product.Id)
										         .Select(s => s.Rate)
										         .AverageAsync();

			product.AverageRating = (decimal)averageRating;
			_unitOfWork.Products.Update(product);

			await _unitOfWork.CompleteAsync();
		}
	}
}
