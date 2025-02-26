namespace Identity.API.Models.AuthModel;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
