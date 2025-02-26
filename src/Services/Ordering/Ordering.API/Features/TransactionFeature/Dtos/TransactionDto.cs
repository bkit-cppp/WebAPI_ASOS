using AutoMapper;

namespace Ordering.API.Features.TransactionFeature.Dtos
{
    public class TransactionDto
    {
        public Guid? Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid? RefundId { get; set; }
        public decimal Total { get; set; }
        public string Content { get; set; }
        public string BankBranch { get; set; }
        public string BankNumber { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Transaction, TransactionDto>();
            }
        }
    }
}
