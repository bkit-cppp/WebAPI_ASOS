using System.Text.Json.Serialization;

namespace Identity.API.Models.AuthModel;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonIgnore] public bool IsAdmin { get; set; } = false;
}
