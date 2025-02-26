namespace Identity.API.Models.UserModel;

public class UserAddOrUpdateRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string? Password { get; set; }
    public bool? IsEmailConfirmed { get; set; }
    public string? StatusId { get; set; }
    public string? RoleId { get; set; }
}
