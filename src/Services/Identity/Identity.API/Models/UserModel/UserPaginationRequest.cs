namespace Identity.API.Models.UserModel;

public class UserPaginationRequest : PaginationRequest
{
	public string? Role { get; set; }
}
