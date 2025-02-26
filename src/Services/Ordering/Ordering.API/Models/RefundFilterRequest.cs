using BuildingBlock.Core.Request;

namespace Ordering.API.Models
{
    public class RefundFilterRequest:FilterRequest
    {
        public Guid? TransactionId { get; set; }
    }
}
