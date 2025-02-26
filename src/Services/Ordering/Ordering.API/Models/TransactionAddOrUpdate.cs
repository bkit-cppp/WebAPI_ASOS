using BuildingBlock.Core.Request;

namespace Ordering.API.Models
{
    public class TransactionAddOrUpdate:AddOrUpdateRequest
    {
        public Guid? Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid? RefundId { get; set; }
        public decimal Total { get; set; }
        public string Content { get; set; }
        public string BankBranch { get; set; }
        public string BankNumber { get; set; }
    }
}
