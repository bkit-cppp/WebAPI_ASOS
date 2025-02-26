using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Catalog.Application.Features.VariationFeature.Consumers;

public class VariationCheckoutComplete : IConsumer<OrderCheckoutEvent>
{
	private readonly IUnitOfWork _unitOfWork;
	public VariationCheckoutComplete(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task Consume(ConsumeContext<OrderCheckoutEvent> consumer)
	{
		foreach (var item in consumer.Message.Variations)
		{
			var variation = await _unitOfWork.Variations.Queryable().FirstOrDefaultAsync(s => s.Id == item.VariationId);
			if (variation == null)
			{
				return;
			}
			variation.QtyDisplay = variation.QtyDisplay - item.Quantity;

			_unitOfWork.Variations.Update(variation);
		}

		await _unitOfWork.CompleteAsync();
	}
}


