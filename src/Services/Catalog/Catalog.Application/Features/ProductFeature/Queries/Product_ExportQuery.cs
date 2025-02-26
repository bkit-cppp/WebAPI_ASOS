using Catalog.Application.Features.ProductFeature.Dto;
using MediatR;
using OfficeOpenXml;

namespace Catalog.Application.Features.ProductFeature.Queries
{
    public record Product_ExportQuery(ExportRequest Request) : IRequest<byte[]>;
    public class Product_ExportQueryHandler : IRequestHandler<Product_ExportQuery, byte[]>
    {
        private readonly IUnitOfWork _uniofWork;
        private readonly IMapper _mapper;
        public Product_ExportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniofWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<byte[]> Handle(Product_ExportQuery request, CancellationToken cancellationToken)
        {
             ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var products = await _uniofWork.Products.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            // Generate Excel using EPPlus or another library
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");

                // Add headers
                
                worksheet.Cells[1, 1].Value = "Product Name";
                worksheet.Cells[1, 2].Value = "Decription";
                worksheet.Cells[1, 3].Value = "AverageRating";
                worksheet.Cells[1, 4].Value = "Slug";

                // Add product data
                for (int i = 0; i < productDtos.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = productDtos[i].Name;
                    worksheet.Cells[i + 2, 2].Value = productDtos[i].Description;
                    worksheet.Cells[i + 2, 3].Value = productDtos[i].AverageRating;
                    worksheet.Cells[i + 2, 4].Value = productDtos[i].Slug;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
