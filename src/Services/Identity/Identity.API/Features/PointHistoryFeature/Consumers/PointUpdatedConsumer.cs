using MassTransit;

namespace Identity.API.Features.PointHistoryFeature.Consumers;

public class PointUpdatedConsumer : IConsumer<PointUpdatedConsumer>
{
	private readonly DataContext _context;
	public PointUpdatedConsumer(DataContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<PointUpdatedConsumer> consumer)
	{
	}
}
