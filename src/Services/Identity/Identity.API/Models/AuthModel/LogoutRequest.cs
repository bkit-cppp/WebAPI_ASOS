using System.Text.Json.Serialization;

namespace Identity.API.Models.AuthModel;

public class LogoutRequest
{
    public Guid Id { get; set; }
    public string Token { get; set; }
}
