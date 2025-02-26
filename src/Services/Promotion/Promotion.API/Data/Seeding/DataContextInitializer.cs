using Promotion.API.Data.Seeding;

namespace Identity.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}

	public async Task<int> InitDiscount()
	{
		int rows = 0;
		if (!_context.Discounts.Any())
		{
			var discounts = new List<Discount>
			{
				new Discount
				{
					Id = Guid.NewGuid(),
					Code = "FREESHIP",
					Value = 15,
					Minimum = 0,
					DiscountTypeId = DiscountTypeConstant.Money,
					StartDate = DateTime.Now.AddMonths(-1),
					EndDate = DateTime.Now.AddMonths(1),
					DeleteFlag = false,
					CreatedUser = Guid.NewGuid(),
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Discount
				{
					Id = Guid.NewGuid(),
					Code = "ASOSCODE",
					Value = 5,
					Minimum = 0,
					DiscountTypeId = DiscountTypeConstant.Money,
					StartDate = DateTime.Now.AddMonths(-1),
					EndDate = DateTime.Now.AddMonths(1),
					DeleteFlag = false,
					CreatedUser = Guid.NewGuid(),
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Discount
				{
					Id = Guid.NewGuid(),
					Code = "SALECODE",
					Value = 20,
					Minimum = 100,
					DiscountTypeId = DiscountTypeConstant.Product,
					StartDate = DateTime.Now.AddMonths(-1),
					EndDate = DateTime.Now.AddMonths(1),
					DeleteFlag = false,
					CreatedUser = Guid.NewGuid(),
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},
				new Discount
				{
					Id = Guid.NewGuid(),
					Code = "HAPPYBIRTHDAY",
					Value = 5,
					Minimum = 50,
					DiscountTypeId = DiscountTypeConstant.Percentage,
					StartDate = DateTime.Now.AddMonths(-1),
					EndDate = DateTime.Now.AddMonths(1),
					DeleteFlag = false,
					CreatedUser = Guid.NewGuid(),
					CreatedDate=DateTime.Now,
					ModifiedDate = DateTime.UtcNow,
					ModifiedUser = Guid.NewGuid()
				},

			};

			_context.Discounts.AddRange(discounts);
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task<int> InitDiscountType()
	{
		int rows = 0;
		if (!_context.DiscountTypes.Any())
		{
			var percentage = new DiscountType()
			{
				Id = DiscountTypeConstant.Percentage,
				Name = DiscountTypeConstant.Percentage,
				Description = ""
			};
			var money = new DiscountType()
			{
				Id = DiscountTypeConstant.Money,
				Name = DiscountTypeConstant.Money,
				Description = ""
			};
			var product = new DiscountType()
			{
				Id = DiscountTypeConstant.Product,
				Name = DiscountTypeConstant.Product,
				Description = ""
			};
			_context.DiscountTypes.Add(percentage);
			_context.DiscountTypes.Add(money);
			_context.DiscountTypes.Add(product);

			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitDiscountType();
			await InitDiscount();
		}
		catch(Exception ex) { }
	}
}
