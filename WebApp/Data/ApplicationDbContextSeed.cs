using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext,
    ILogger logger,
    int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (dbContext.Database.IsSqlServer())
                {
                    dbContext.Database.Migrate();
                }

                //if (!await dbContext.CatalogBrands.AnyAsync())
                //{
                //    await dbContext.CatalogBrands.AddRangeAsync(
                //        GetPreconfiguredCatalogBrands());

                //    await dbContext.SaveChangesAsync();
                //}
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(dbContext, logger, retryForAvailability);
                throw;
            }
        }

    }
}
