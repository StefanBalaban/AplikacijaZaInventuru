using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AppContextSeed
    {
        public static async Task SeedAsync(AppContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            var retryForAvailability = retry.Value;
            try
            {
                if (!await catalogContext.UnitOfMeasures.AnyAsync())
                {
                    await catalogContext.UnitOfMeasures.AddRangeAsync(
                        new UnitOfMeasure(1));

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.FoodProducts.AnyAsync())
                {
                    var ids = 1;
                    await catalogContext.FoodProducts.AddRangeAsync(
                        new Faker<FoodProduct>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.UnitOfMeasureId, f => 1)
                            .GenerateBetween(12, 12));

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.FoodStock.AnyAsync())
                {
                    var ids = 1;
                    await catalogContext.FoodStock.AddRangeAsync(
                        new Faker<FoodStock>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.FoodProductId, f => 1)
                            .GenerateBetween(12, 12));

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<AppContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }

                throw;
            }
        }
    }
}