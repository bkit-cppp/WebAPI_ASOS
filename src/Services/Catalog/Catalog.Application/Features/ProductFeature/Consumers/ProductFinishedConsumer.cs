using BuildingBlock.Constants;
using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Catalog.Application.Features.ProductFeature.Consumers;

public class ProductFinishedConsumer : IConsumer<OrderStatusUpdatedEvent>
{
	private readonly IUnitOfWork _unitOfWork;
	public ProductFinishedConsumer(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task Consume(ConsumeContext<OrderStatusUpdatedEvent> consumer)
	{
		if(consumer.Message.StatusId != OrderStatusConstant.Completed && consumer.Message.StatusId != OrderStatusConstant.Canceled)
		{
			return;
		}

		foreach(var item in consumer.Message.Products)
		{
			var variation = await _unitOfWork.Variations.Queryable().FirstOrDefaultAsync(s => s.Id == item.VariationId);
			if (variation == null)
			{
				return;
			}

			if (consumer.Message.StatusId != OrderStatusConstant.Completed) 
			{
				variation.QtyInStock = variation.QtyInStock - item.Quantity;
			}
			else
			{
				variation.QtyDisplay = variation.QtyDisplay + item.Quantity;
			}

			var product = await _unitOfWork.Products.Queryable().FirstOrDefaultAsync(s => s.Id == item.ProductId);
			if (product == null)
			{
				return;
			}

			if (consumer.Message.StatusId != OrderStatusConstant.Completed)
			{
				product.Bought = product.Bought + item.Quantity;
			}
			else
			{
				product.Bought = product.Bought - item.Quantity;
			}
			

			_unitOfWork.Variations.Update(variation);
			_unitOfWork.Products.Update(product);
		}

		await _unitOfWork.CompleteAsync();
	}
}

