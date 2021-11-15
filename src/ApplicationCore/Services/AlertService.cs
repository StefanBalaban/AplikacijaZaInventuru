using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.DietPlanPeriodSpecs;
using ApplicationCore.Specifications.DietPlanSpecs;
using ApplicationCore.Specifications.FoodStockSpecs;
using ApplicationCore.Specifications.NotificationRuleSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AlertService : IAlertService
    {
        // TODO: Too many dependencies in one class, change flow as to reduce the number of deps to 2-3 per class
        private readonly IDietPlanPeriodService _dietPlanPeriodService;
        private readonly IDietPlanService _dietPlanService;
        private readonly INotificationRuleService _notificationRuleService;
        private readonly IFoodStockService _foodStockService;
        private readonly IFoodProductService _foodProductService;
        private readonly IEmailSender _emailSender;

        public AlertService(IEmailSender emailSender, INotificationRuleService notificationRuleService, IDietPlanPeriodService dietPlanPeriodService, IDietPlanService dietPlanService, IFoodStockService foodStockService, IFoodProductService foodProductService)
        {
            _dietPlanPeriodService = dietPlanPeriodService;
            _dietPlanService = dietPlanService;
            _foodStockService = foodStockService;
            _foodProductService = foodProductService;
            _notificationRuleService = notificationRuleService;
            _emailSender = emailSender;
        }

        private async Task<List<FoodStock>> FoodStocksWithLowAmountAsync(DateTime date)
        {
            var dietPlanPeriods = (await _dietPlanPeriodService.ListAll()).Where(x => x.EndDate >= date && x.StartDate <= date).ToList();

            if (dietPlanPeriods.Count == 0)
            {
                return new List<FoodStock>();
            }

            var dietPlans = await _dietPlanService.GetAsync(new DietPlanFilterPaginatedSpecification(null, 0, 1000, null), new DietPlanFilterPaginatedSpecification(null, 0, 1000, null));


            if (dietPlans.List.Where(x => dietPlanPeriods.Any(y => y.DietPlanId == x.Id)).Count() == 0)
            {
                return new List<FoodStock>();
            }

            var notificationRules = await _notificationRuleService.GetAsync(new NotificationRuleFilterPaginatedSpecification(null, 0, 1000, null), new NotificationRuleFilterPaginatedSpecification(null, 0, 1000, null));
            var foodProducts = new List<FoodProduct>();

            foreach (var dietPlan in dietPlans.List)
            {
                foreach (var dietPlanMeal in dietPlan.DietPlanMeals)
                {
                    foreach (var meal in dietPlanMeal.Meal.Meals)
                    {
                        if (notificationRules.List.Any(x => x.FoodProductId == meal.FoodProductId))
                        {
                            foodProducts.Add(meal.FoodProduct);
                        }
                    }
                }
            }

            if (foodProducts.Count() == 0)
            {
                return new List<FoodStock>();
            }

            var foodStock = await _foodStockService.GetAsync(new FoodStockFilterPaginatedSpecification(null, 0, 1000, null), new FoodStockFilterPaginatedSpecification(null, 0, 1000, null));

            return foodStock.List.Where(x => foodProducts.Any(y => y.Id == x.FoodProductId) && x.Amount < x.LowerAmount).ToList();
        }

        public async Task LowerAmountForDay(DateTime date)
        {
            var dietPlanPeriods = (await _dietPlanPeriodService.ListAll()).Where(x => x.EndDate >= date && x.StartDate <= date).ToList();

            if (dietPlanPeriods.Count == 0)
            {
                return;
            }
            var dietPlans = await _dietPlanService.GetAsync(new DietPlanFilterPaginatedSpecification(null, 0, 1000, null), new DietPlanFilterPaginatedSpecification(null, 0, 1000, null));

            if (dietPlans.List.Where(x => dietPlanPeriods.Any(y => y.DietPlanId == x.Id)).Count() == 0)
            {
                return;
            }

            var notificationRules = await _notificationRuleService.GetAsync(new NotificationRuleFilterPaginatedSpecification(null, 0, 1000, null), new NotificationRuleFilterPaginatedSpecification(null, 0, 1000, null));
            var foodProducts = new List<FoodProduct>();
            var dietPlanMealAmount = new Dictionary<int, float>();
            foreach (var dietPlan in dietPlans.List)
            {
                foreach (var dietPlanMeal in dietPlan.DietPlanMeals)
                {
                    foreach (var meal in dietPlanMeal.Meal.Meals)
                    {
                        if (notificationRules.List.Any(x => x.FoodProductId == meal.FoodProductId))
                        {
                            foodProducts.Add(meal.FoodProduct);
                            if (dietPlanMealAmount.TryGetValue(meal.FoodProductId, out float amount))
                            {
                                dietPlanMealAmount[meal.FoodProductId] += meal.Amount;
                            }
                            else
                            {
                                dietPlanMealAmount.Add(meal.FoodProductId, meal.Amount);
                            }

                        }
                    }
                }
            }
            var foodStocks = await _foodStockService.GetAsync(new FoodStockFilterPaginatedSpecification(null, 0, 1000, null), new FoodStockFilterPaginatedSpecification(null, 0, 1000, null));
            var updatedFoodStocks = new List<FoodStock>();

            foreach (var item in foodProducts)
            {
                var foodStock = foodStocks.List.SingleOrDefault(x => x.FoodProductId == item.Id);
                if (foodStock != null)
                {
                    if (dietPlanMealAmount.TryGetValue(foodStock.FoodProductId, out float amount))
                    {
                        foodStock.Amount -= amount;
                        if (foodStock.Amount < 0) 
                        { 
                            foodStock.Amount = 0; 
                        }
                        await _foodStockService.PutAsync(foodStock);
                    }
                }
            }

        }

        public async Task SendEmailAlerts()
        {
            var foodStocks = await FoodStocksWithLowAmountAsync(DateTime.Today);
            var notificationRules = await _notificationRuleService.GetAsync(new NotificationRuleFilterPaginatedSpecification(null, 0, 1000, null), new NotificationRuleFilterPaginatedSpecification(null, 0, 1000, null));
            foreach (var foodStock in foodStocks)
            {

                var userContact = notificationRules.List.SingleOrDefault(x => x.UserId == foodStock.UserId);
                if (userContact != null && userContact.NotificationRuleUserContactInfos?.FirstOrDefault() != null)
                {
                    var email = userContact.NotificationRuleUserContactInfos.FirstOrDefault().UserContactInfo.Contact;
                    var foodProduct = await _foodProductService.GetAsync(foodStock.FoodProductId);
                    _ = _emailSender.SendEmailAsync(email, $"Niske zalihe za {foodProduct.Name}", $"Trenutne zalihe za {foodProduct.Name} su na {foodStock.Amount}");
                }

            }
        }
    }
}
