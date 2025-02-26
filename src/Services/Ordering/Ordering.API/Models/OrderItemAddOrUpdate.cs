using BuildingBlock.Core.Request;

namespace Ordering.API.Models
{
    public class OrderItemAddOrUpdate:AddOrUpdateRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public decimal Stock { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Amount { get; set; } = 0;
    }
}
