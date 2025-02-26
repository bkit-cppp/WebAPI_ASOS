using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Installers;

public class MigrationChecker
{
	private readonly DbContext _dbContext;

	public MigrationChecker(DbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void CheckAndUpdateDatabase()
	{
		try
		{
			var migrator = _dbContext.GetService<IMigrator>();
			var pendingMigrations = _dbContext.Database.GetPendingMigrations();

			if (pendingMigrations.Any())
			{
				Console.WriteLine("*** [ There are pending migrations, updating the database... ] ***");
				migrator.Migrate();
			}
			else
			{
				Console.WriteLine("*** [ No pending migrations. The database is up to date. ] ***");
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine($"*** [ MigrationChecker ] - Error: {ex.Message} ***");
		}
		
	}
}
public static class MigrationCheckerExtensions
{
	public static WebApplication MigrationAutoUpdate<TContext>(this WebApplication app)
		where TContext : DbContext
	{
		using (var scope = app.Services.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
			var migrationChecker = new MigrationChecker(dbContext);
			migrationChecker.CheckAndUpdateDatabase();
			scope.Dispose();
		}
		return app;
	}
}