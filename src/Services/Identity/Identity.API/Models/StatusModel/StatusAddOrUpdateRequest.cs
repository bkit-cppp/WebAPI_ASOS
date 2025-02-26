namespace Identity.API.Models.StatusModel;

public class StatusAddOrUpdateRequest : AddOrUpdateRequest
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
