namespace Identity.API.Constants;

public static class OTPConstant
{
	public static int Minute { get; } = 5;
	public static string Email { get; } = "namdinhtuan5@gmail.com";
	public static string Key { get; } = "dhxe drjt okgr fdhb";
	public static string RegisterType { get; } = nameof(RegisterType);
	public static string ForgetPasswordType { get; } = nameof(ForgetPasswordType);

	public static DateTime EmailValidTo()
	{
		return DateTime.Now.AddMinutes(Minute);
	}

	public static string EmailTemplate(string code)
	{
		var verifyUrl = $"{Program.BaseUrl}/api/Auth/verify-email/{code}";
		string htmlContent = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Xác Nhận Mã</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        padding: 20px;
                    }}
                    .container {{
                        background-color: #ffffff;
                        border-radius: 5px;
                        padding: 20px;
                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                    }}
                    .code {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #4CAF50;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Xác Nhận Mã</h2>
                    <p>Chào bạn,</p>
                    <p>Để xác nhận tài khoản của bạn, hãy sử dụng mã xác nhận bên dưới:</p>
                    <p class='code'>{code}</p>
                    <p>Mã này sẽ hết hạn trong <strong>{Minute}</strong> phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                    <p>Hoặc bạn có thể truy cập đường link sau để xác thực:</p>
                    <p><strong>{verifyUrl}</strong></p>
                    <p>Cảm ơn bạn!</p>
                </div>
            </body>
            </html>
            ";

		return htmlContent;
	}

	public static string ForgetPasswordTemplate(string code)
	{
		var verifyUrl = $"{Program.BaseUrl}/api/Auth/verify-forget-password/{code}";
		string htmlContent = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Mã xác nhận quên mật khẩu</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        padding: 20px;
                    }}
                    .container {{
                        background-color: #ffffff;
                        border-radius: 5px;
                        padding: 20px;
                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                    }}
                    .code {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #4CAF50;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Mã xác nhận QUÊN MẬT KHẨU</h2>
                    <p>Chào bạn,</p>
                    <p>Để xác nhận quên mật khẩu, bạn hãy sử dụng mã xác nhận bên dưới:</p>
                    <p class='code'>{code}</p>
                    <p>Mã này sẽ hết hạn trong <strong>{Minute}</strong> phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                    <p>Hoặc bạn có thể truy cập đường link sau để xác thực:</p>
                    <p><strong>{verifyUrl}</strong></p>
                    <p>Cảm ơn bạn!</p>
                </div>
            </body>
            </html>
            ";

		return htmlContent;
	}

	public static string OrderCheckoutCompleteTemplate(string id,string fullName)
	{
		string htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""vi"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Chúc Mừng Đặt Hàng Thành Công - ASOS</title>
                <style>
                    body {{
                        font-family: 'Arial', sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}

                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                    }}

                    .header {{
                        text-align: center;
                        background-color: #004C97;
                        color: #ffffff;
                        padding: 20px;
                        border-radius: 8px 8px 0 0;
                    }}

                    .header h1 {{
                        margin: 0;
                        font-size: 32px;
                    }}

                    .content {{
                        margin-top: 20px;
                        padding: 20px;
                    }}

                    .content p {{
                        font-size: 16px;
                        line-height: 1.6;
                    }}

                    .order-id {{
                        background-color: #e9f7fe;
                        padding: 10px;
                        margin-top: 15px;
                        font-size: 18px;
                        font-weight: bold;
                        color: #004C97;
                        border-radius: 4px;
                    }}

                    .footer {{
                        text-align: center;
                        margin-top: 40px;
                        font-size: 14px;
                        color: #777777;
                    }}

                    .footer a {{
                        color: #004C97;
                        text-decoration: none;
                    }}

                    .footer p {{
                        margin: 5px 0;
                    }}

                    .btn {{
                        display: inline-block;
                        background-color: #004C97;
                        color: #ffffff;
                        padding: 12px 30px;
                        border-radius: 25px;
                        text-decoration: none;
                        font-size: 16px;
                        margin-top: 20px;
                    }}

                    .btn:hover {{
                        background-color: #003366;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Chúc Mừng Đặt Hàng Thành Công!</h1>
                    </div>
                    <div class=""content"">
                        <p>Xin chào <strong>{fullName}</strong>,</p>
                        <p>Chúng tôi rất vui mừng thông báo rằng đơn hàng của bạn đã được đặt thành công tại ASOS!</p>
                        <p>Thông tin đơn hàng của bạn:</p>
                        <div class=""order-id"">
                            <p><strong>ID Đơn Hàng:</strong> {id}</p>
                        </div>
                        <p>Cảm ơn bạn đã tin tưởng lựa chọn sản phẩm tại ASOS. Đơn hàng của bạn đang được xử lý và sẽ được gửi đến bạn trong thời gian sớm nhất.</p>
                        <a href=""{{OrderTrackingUrl}}"" class=""btn"">Theo Dõi Đơn Hàng</a>
                    </div>
                    <div class=""footer"">
                        <p>ASOS - Shop Online</p>
                        <p><a href=""{{UnsubscribeUrl}}"">Hủy Đăng Ký</a></p>
                    </div>
                </div>
            </body>
            </html>
            ";

		return htmlContent;
	}
}
