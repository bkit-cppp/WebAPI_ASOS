namespace Identity.API.Models.AuthModel;

public class UpdatePasswordRequest : AddOrUpdateRequest
{
    public Guid Id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
