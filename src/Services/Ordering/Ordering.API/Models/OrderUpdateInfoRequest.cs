using System.Text.Json.Serialization;

namespace Ordering.API.Models;

public class OrderUpdateInfoRequest
{
	public Guid Id { get; set; }
	public string ReceiverName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
	[JsonIgnore] public Guid ModifiedUser { get; set; }
}
