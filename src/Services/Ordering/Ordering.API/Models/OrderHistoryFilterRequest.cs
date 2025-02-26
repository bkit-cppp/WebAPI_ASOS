using BuildingBlock.Core.Request;

namespace Ordering.API.Models
{
    public class OrderHistoryFilterRequest:FilterRequest
    {
        public Guid? OrderId { get; set; }
    }
}
