using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Catalog.Application.Features.UserCommentFeature.Consumers;

public sealed class UserUpdatedConsumer : IConsumer<UserUpdatedEvent>
{
	private readonly IUnitOfWork _unitOfWork;
	public UserUpdatedConsumer(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Consume(ConsumeContext<UserUpdatedEvent> consumer)
	{
		var user = await _unitOfWork.UserComments.FindAsync(consumer.Message.Id);
		if(user != null)
		{
			user.Email = consumer.Message.Email;
			user.Fullname = consumer.Message.Fullname;
			user.Avatar = consumer.Message.Avatar;
			_unitOfWork.UserComments.Update(user);
			await _unitOfWork.CompleteAsync();
		}
	}
}
