using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Data;

[Table("tb_otps")]
public class OTP : BaseEntity<Guid>
{
	public OTP() : base()
	{
		Id = Guid.NewGuid();
		ValidTo = OTPConstant.EmailValidTo();
	}
	public Guid? UserId { get; set; }
	public User? User { get; set; }
	public string From { get; set; } = string.Empty;
	public string To { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public string Code { get; set; } = string.Empty;
	public DateTime ValidTo { get; set; }
	public bool IsExpired { get; set; } = false;
	public string Type { get; set; } = "Email";
}
