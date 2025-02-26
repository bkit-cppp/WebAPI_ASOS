using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuildingBlock.Core.Request;

public  class BaseRequest 
{
	public string? TextSearch { get; set; }
	public string? OrderCol { get; set; }
	public string? OrderDir { get; set; }
	public string? Status { get; set; }
	public string? Type { get; set; }
	public string? Date { get; set; }
	[BindNever] public Guid? UserId { get; set; }
	[BindNever] public string? RoleId { get; set; }
}

public static class OrderDir
{
	public static string Desc { get; } = nameof(Desc);
	public static string Asc { get; } = nameof(Asc);
}