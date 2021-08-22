using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Entities.NotificationAggregate;
using ApplicationCore.Entities.UserAggregate;
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
                    var mealItems = new Faker<MealItem>().RuleFor(m => m.Id, f => mIds++).RuleFor(m => m.FoodProductId, f => 1).GenerateBetween(12, 12);
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

                if (!await catalogContext.DietPlanPeriod.AnyAsync())
                {
                    var ids = 1;
                    await catalogContext.DietPlanPeriod.AddRangeAsync(
                        new Faker<DietPlanPeriod>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.EndDate, f => new DateTime(2001, 2, 1)).RuleFor(m => m.StartDate, f => new DateTime(2001, 1, 1))
                            .GenerateBetween(12, 12));

                    var dietPlanPeriods = await catalogContext.DietPlanPeriod.ToListAsync();
                    var i = 1;
                    foreach (var dietPlanPeriod in dietPlanPeriods)
                    {
                        dietPlanPeriod.DietPlanId = i++;
                    };

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.User.AnyAsync())
                {

                    var ids = 1;
                    await catalogContext.User.AddRangeAsync(
                        new Faker<User>().RuleFor(m => m.Id, f => ids++).RuleFor(x => x.FirstName, f => "Stef").RuleFor(x => x.UserContactInfos, f => new Faker<UserContactInfo>().RuleFor(y => y.Contact, f => "stef.balaban@email.com").GenerateBetween(12, 12))
                            .GenerateBetween(12, 12));

                    await catalogContext.SaveChangesAsync();
                    await catalogContext.UserContactInfo.AddAsync(new Faker<UserContactInfo>().RuleFor(x => x.UserId, f => 1).Generate());
                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.NotificationRule.AnyAsync())
                {
                    
                    var notifUserInfo = new Faker<NotificationRuleUserContactInfos>().RuleFor(x => x.UserContactInfosId, f => 1).GenerateBetween(12, 12);
                    var ids = 1;
                    await catalogContext.NotificationRule.AddRangeAsync(
                        new Faker<NotificationRule>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.FoodProductId, 1)
                            .GenerateBetween(12, 12));
                    await catalogContext.SaveChangesAsync();
                    var notificationRules = await catalogContext.NotificationRule.ToListAsync();

                    var id = 0;
                    foreach (var notificationRule in notificationRules)
                    {
                        notificationRule.NotificationRuleUserContactInfos = new List<NotificationRuleUserContactInfos> { notifUserInfo[id++]};
                    }

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.UserWeightEvidention.AnyAsync())
                {                    
                    var ids = 1;
                    await catalogContext.UserWeightEvidention.AddRangeAsync(
                        new Faker<UserWeightEvidention>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.Weight, 12).RuleFor(m => m.EvidentationDate, f => new DateTime(2001, 1, 1))
                            .GenerateBetween(12, 12));
                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.UserSubscription.AnyAsync())
                {
                    var ids = 1;
                    await catalogContext.UserSubscription.AddRangeAsync(
                        new Faker<UserSubscription>().RuleFor(m => m.Id, f => ids++).RuleFor(m => m.BegginDate, new DateTime(2001, 1, 1))
                        .RuleFor(m => m.EndDate, new DateTime(2001, 2, 1))
                        .RuleFor(m => m.PaymentDate, new DateTime(2001, 1, 1))
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