using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.SizeCategoryFeature.Dto
{
    public class SizeCategoryDto
    {
        public Guid Id { get; set; }
        public string? SizeId { get; set; }
        public Size? Size { get; set; }
        public string? CategoryId { get; set; }
        public SizeCategory? SizeCategory { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<SizeCategory, SizeCategoryDto>();
            }
        }
    }
}
