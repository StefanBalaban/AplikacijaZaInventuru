using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
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

                if (!await catalogContext.Meals.AnyAsync())
                { 
                    var mIds = 1;
                    var mealItems = new Faker<MealItem>().RuleFor(m => m.Id, f => mIds++).RuleFor(m => m.FoodProductId, f=> 1).GenerateBetween(12, 12);
                    var ids = 1;
                    await catalogContext.Meals.AddRangeAsync(
                        new Faker<Meal>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.Meals, f => mealItems).RuleFor(m => m.Name, f => "Dorucak")
                            .GenerateBetween(12, 12));

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.DietPlans.AnyAsync())
                {
                    var meals = await catalogContext.Meals.ToListAsync();
                    var ids = 1;
                    await catalogContext.DietPlans.AddRangeAsync(
                        new Faker<DietPlan>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.Name, f => "Sedmica").GenerateBetween(12, 12));

                    var dietPlans = await catalogContext.DietPlans.ToListAsync();

                    var i = 1;
                    foreach (var dietPlan in dietPlans)
                    {
                        catalogContext.DietPlanMeal.Add(new DietPlanMeal { DietPlan = dietPlan, Meal = meals[i++] });
                    };

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