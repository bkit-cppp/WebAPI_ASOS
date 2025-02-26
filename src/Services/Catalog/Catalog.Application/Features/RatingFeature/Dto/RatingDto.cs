namespace Catalog.Application.Features.RatingFeature.Dto;

public class RatingDto
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
    public int Rate { get; set; }
    public Guid? UserId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Rating, RatingDto>();
        }
    }
}
