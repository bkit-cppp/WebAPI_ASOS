using BuildingBlock.Messaging.Events;
using MailKit.Net.Smtp;
using MassTransit;
using MimeKit;

namespace Identity.API.Features.OTPFeature.Consumers;

public class PushEmailConsumer : IConsumer<PushEmailEvent>
{
	private readonly DataContext _context;
	public PushEmailConsumer(DataContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<PushEmailEvent> consumer)
	{
		if(consumer.Message.Type == OTPConstant.RegisterType)
		{
			await HandlerRegisterEmail(consumer);
		}
		if (consumer.Message.Type == OTPConstant.ForgetPasswordType)
		{
			await HandlerForgetPasswordEmail(consumer);
		}
	}

	private async Task HandlerRegisterEmail(ConsumeContext<PushEmailEvent> consumer)
	{
		var user = await _context.Users.FindAsync(consumer.Message.UserId);
		if (user != null)
		{
			string code = GenerateRandomCode();
			var otp = new OTP()
			{
				UserId = user.Id,
				From = OTPConstant.Email,
				To = consumer.Message.Email,
				Code = code,
				Content = "Mã xác nhận tài khoản ASOS",
				Type = OTPConstant.RegisterType,
				IsExpired = false,
				CreatedUser = user.Id
			};
			_context.OTPs.Add(otp);
			int rows = await _context.SaveChangesAsync();

			if (rows > 0) 
			{
				await SendEmail(
					consumer.Message.Email,
					"Mã xác nhận tài khoản ASOS", 
					OTPConstant.EmailTemplate(code)
				);
			} 
		}
	}

	private async Task HandlerForgetPasswordEmail(ConsumeContext<PushEmailEvent> consumer)
	{
		var user = await _context.Users.FindAsync(consumer.Message.UserId);
		if (user != null)
		{
			string code = GenerateRandomCode();
			var otp = new OTP()
			{
				UserId = user.Id,
				From = OTPConstant.Email,
				To = consumer.Message.Email,
				Code = code,
				Content = "Mã xác nhận quên mật khẩu ASOS",
				Type = OTPConstant.ForgetPasswordType,
				IsExpired = false,
				CreatedUser = user.Id
			};
			_context.OTPs.Add(otp);
			int rows = await _context.SaveChangesAsync();

			if (rows > 0)
			{
				await SendEmail(
					consumer.Message.Email,
					"Mã xác nhận quên mật khẩu ASOS",
					OTPConstant.ForgetPasswordTemplate(code)
				);
			}
		}
	}

	private async Task SendEmail(string email,string subject, string body)
	{
		var message = new MimeMessage();
		message.From.Add(new MailboxAddress("ASOS", OTPConstant.Email));
		message.To.Add(new MailboxAddress("Tài khoản ASOS", email));
		message.Subject = subject;

		message.Body = new TextPart("html")
		{
			Text = body
		};

		using (var client = new SmtpClient())
		{
			client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

			client.Authenticate(OTPConstant.Email, OTPConstant.Key);

			await client.SendAsync(message);
			client.Disconnect(true);
		}
	}

	private string GenerateRandomCode()
	{
		Random random = new Random();
		int code = random.Next(100000, 999999);
		return code.ToString();
	}

}
