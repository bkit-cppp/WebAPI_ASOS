namespace Identity.API.Models.AuthModel;

public class ProfileUpdateRequest
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string? Avatar { get; set; }
}
