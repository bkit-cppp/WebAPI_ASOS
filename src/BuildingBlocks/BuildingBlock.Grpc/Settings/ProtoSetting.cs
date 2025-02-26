namespace BuildingBlock.Grpc.Settings;

public class ProtoSetting
{
	public bool? Identity { get; set; } = false;
	public bool? Catalog { get; set; } = false;
	public bool? Basket { get; set; } = false;
	public bool? Ordering { get; set; } = false;
	public bool? Promotion { get; set; } = false;
	public bool? Event { get; set; } = false;
	public bool? Storage { get; set; } = false;
}
