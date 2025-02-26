using BuildingBlock.Core.Request;

namespace Ordering.API.Models
{
    public class RefundAddOrUpdateRequest:AddOrUpdateRequest
    {
        public Guid ? Id { get; set; }
        public decimal RefundAmount { get; set; }
        public string Reason { get; set; }
        public Guid TransactionId { get; set; }
    }
}
