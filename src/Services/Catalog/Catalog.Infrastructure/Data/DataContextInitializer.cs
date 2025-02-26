namespace Catalog.Infrastructure.Data;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}

	public async Task InitBrand()
	{
		if (!_context.Brands.Any())
		{
			var brands = new List<Brand>
			{
				new Brand
				{
					Id = Guid.NewGuid(),
					Description = "ASOS Collection",
					Name = "ASOS Collection",
					Slug = "asos-collection",
					Image="https://res.cloudinary.com/dczpqymrv/image/upload/v1730879241/oasrhismuuuebohviyvk.png",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Brand
				{
					Id = Guid.NewGuid(),
					Description = "Gucci",
					Name = "Gucci",
					Slug = "gucci",
					Image="https://bota.vn/wp-content/uploads/2018/11/gg.jpg",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Brand
				{
					Id = Guid.NewGuid(),
					Description = "Dior",
					Name = "Dior",
					Slug = "dior",
					Image="https://cdn.shopify.com/s/files/1/0558/6413/1764/files/Rewrite_Dior_Logo_Design_History_Evolution_0_1024x1024.jpg?v=1694703764",
					CreatedUser = Guid.NewGuid(),
					CreatedDate=DateTime.UtcNow,
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Brand
				{
					Id = Guid.NewGuid(),
					Description = "Nike",
					Name = "Nike",
					Slug = "nike",
					Image="https://inkythuatso.com/uploads/thumbnails/800/2021/11/logo-nike-inkythuatso-2-01-04-15-43-59.jpg",
					CreatedUser = Guid.NewGuid(),
					CreatedDate=DateTime.UtcNow,
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				}
			};

			_context.Brands.AddRange(brands);
			await _context.SaveChangesAsync();
		}
	}

	public async Task InitCategory()
	{
		if (!_context.Categories.Any())
		{
			var categories = new List<Category>
			{
				new Category
				{
					Id = Guid.NewGuid(),
					Description = "Clothing",
					Name = "Clothing",
					Slug = "clothing",
					ParentId = null,
					ImageFile="",
					CreatedDate=DateTime.UtcNow,
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Category
				{
					Id = Guid.NewGuid(),
					Description = "Shoes",
					Name = "Shoes",
					Slug = "shoes",
					ParentId = null,
					ImageFile="",
					CreatedDate=DateTime.UtcNow,
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Category
				{
					Id = Guid.NewGuid(),
					Description = "Jacket",
					Name = "Jacket",
					Slug = "jacket",
					ParentId = null,
					ImageFile="",
					CreatedDate=DateTime.UtcNow,
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Category
				{
					Id = Guid.NewGuid(),
					Description = "Pants & Trousers",
					Name = "Pants & Trousers",
					Slug = "pants-and-trousers",
					ParentId = null,
					ImageFile="",
					CreatedDate=DateTime.UtcNow,
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Category
				{
					Id = Guid.NewGuid(),
					Description = "Dress",
					Name = "Dress",
					Slug = "dress",
					ParentId = null,
					ImageFile="",
					CreatedDate=DateTime.UtcNow,
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				}
			};

			_context.Categories.AddRange(categories);
			await _context.SaveChangesAsync();
		}
	}

	public async Task InitColor()
	{
		if (!_context.Colors.Any())
		{

			var colors = new List<Color>
			{
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Yellow",
					Name = "Yellow",
					Slug = "yellow",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Red",
					Name = "Red",
					Slug = "red",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Blue",
					Name = "Blue",
					Slug = "blue",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Green",
					Name = "Green",
					Slug = "green",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Black",
					Name = "Black",
					Slug = "black",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "White",
					Name = "White",
					Slug = "white",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Purple",
					Name = "Purple",
					Slug = "purple",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Pink",
					Name = "Pink",
					Slug = "pink",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Orange",
					Name = "Orange",
					Slug = "orange",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Brown",
					Name = "Brown",
					Slug = "brown",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Color
				{
					Id = Guid.NewGuid(),
					Description = "Gray",
					Name = "Gray",
					Slug = "gray",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				}
			};

			_context.Colors.AddRange(colors);
			await _context.SaveChangesAsync();
		}
	}

	public async Task InitProduct()
	{
		if (!_context.Products.Any())
		{
			var products = new List<Product>
			{
				new Product
				{
					Id = Guid.NewGuid(),
					Description="Hang Tot",
					Name = "LapTop Dell Inspiration ",
					AverageRating=5,
					SizeAndFit="XXL",
					Slug = "https://www.facebook.com/groups/thuedoancntt",
					CreatedUser = Guid.NewGuid(),

					CreatedDate=DateTime.UtcNow,

					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Description="Hang Tot",
					Name = "LapTop Gaming ",
					AverageRating=10,
					SizeAndFit="XXXL",

					Slug = "https://www.facebook.com/groups/thuedoancntt",
					CreatedDate=DateTime.UtcNow,
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				}
			};

			_context.Products.AddRange(products);
			await _context.SaveChangesAsync();
		}
	}

	public async Task InitGender()
	{
		if (!_context.Genders.Any())
		{
			var genders = new List<Gender>
			{
				new Gender
				{
					Id = Guid.NewGuid(),
					Description = "Male",
					Name = "Male",
					Slug = "male",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Gender
				{
					Id = Guid.NewGuid(),
					Description = "Female",
					Name = "Female",
					Slug = "female",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
			};

			_context.Genders.AddRange(genders);
			await _context.SaveChangesAsync();
		}
	}

	public async Task InitSize()
	{
		if (!_context.Sizes.Any())
		{
			var sizes = new List<Size>
			{
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "3XL",
					Name = "3XL",
					Slug = "3xl",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "2XL",
					Name = "2XL",
					Slug = "2xl",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "XL",
					Name = "XL",
					Slug = "xl",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "L",
					Name = "L",
					Slug = "l",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "M",
					Name = "M",
					Slug = "m",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "S",
					Name = "S",
					Slug = "s",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "XS",
					Name = "XS",
					Slug = "xs",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 6",
					Name = "US 6",
					Slug = "us-6",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 7",
					Name = "US 7",
					Slug = "us-7",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 8",
					Name = "US 8",
					Slug = "us-8",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 9",
					Name = "US 9",
					Slug = "us-9",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 10",
					Name = "US 10",
					Slug = "us-10",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 11",
					Name = "US 11",
					Slug = "us-11",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Size
				{
					Id = Guid.NewGuid(),
					Description = "US 12",
					Name = "US 12",
					Slug = "us-12",
					CreatedUser = Guid.NewGuid(),
					DeleteFlag = false,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				}
			};

			_context.Sizes.AddRange(sizes);
			await _context.SaveChangesAsync();
		}
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitBrand();
			await InitCategory();
			await InitColor();
			await InitProduct();
			await InitGender();
			await InitSize();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error seeding data: {ex.Message}");
		}
	}
}
