using BuildingBlock.Messaging.Events;
using MailKit;
using MailKit.Net.Smtp;
using MassTransit;
using MimeKit;

namespace Identity.API.Features.OTPFeature.Consumers;

public sealed class MailCheckoutComplete : IConsumer<OrderCheckoutEvent>
{
	private readonly DataContext _context;
	public MailCheckoutComplete(DataContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<OrderCheckoutEvent> consumer)
	{
		var user = await _context.Users.FindAsync(consumer.Message.UserId);

		if (user == null)
		{
			return;
		}

		var message = new MimeMessage();
		message.From.Add(new MailboxAddress("ASOS", OTPConstant.Email));
		message.To.Add(new MailboxAddress("ASOS Order", user.Email));
		message.Subject = "Order checkout completed";

		message.Body = new TextPart("html")
		{
			Text = OTPConstant.OrderCheckoutCompleteTemplate(consumer.Message.OrderId.ToString(), user.Fullname)
		};

		using (var client = new SmtpClient())
		{
			client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

			client.Authenticate(OTPConstant.Email, OTPConstant.Key);

			await client.SendAsync(message);
			client.Disconnect(true);
		}
	}
}