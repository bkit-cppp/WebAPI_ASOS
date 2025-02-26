using BuildingBlock.Core.Request;

namespace Ordering.API.Models
{
    public class OrderHistoryAddOrUpdate:AddOrUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Status { get; set; }
    }
}
