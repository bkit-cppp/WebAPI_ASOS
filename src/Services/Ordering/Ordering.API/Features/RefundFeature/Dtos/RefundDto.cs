using AutoMapper;


namespace Ordering.API.Features.RefundFeature.Dtos
{
    public class RefundDto
    {
        public decimal RefundAmount { get; set; }
        public string Reason { get; set; }
        public Guid TransactionId { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Refund, RefundDto>();
            }
        }
    }
}
