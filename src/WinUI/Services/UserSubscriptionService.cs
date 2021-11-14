using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUI.Models;

namespace WinUI.Services
{

    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IApiService _apiService;
        private readonly string endpoint = "usersubscription";
        private List<UserSubscription> _cachedResponse;

        public UserSubscriptionService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<UserSubscription>> GetUserSubscriptionsAsync()
        {
            if (_cachedResponse != null)
            {
                return _cachedResponse;
            }

            var list = await _apiService.GetAsync<UserSubscriptionListResponse>($"{endpoint}?pageSize=1000&index=0");

            _cachedResponse = list.UserSubscriptions;

            return list.UserSubscriptions;
        }

        public async Task<List<NumberOfUserSubscriptionsPerMonth>> GetProfitForUserSubscriptionsForYear(int year)
        {
            if (_cachedResponse == null)
            {
                await GetUserSubscriptionsAsync();
            }

            var subscriptions = _cachedResponse.Where(x => x.PaymentDate.Year == year);

            var numberOfUserSubscriptionsPerMonth = new List<NumberOfUserSubscriptionsPerMonth>();

            for (int i = 1; i <= 12; i++)
            {
                numberOfUserSubscriptionsPerMonth.Add(
                    new NumberOfUserSubscriptionsPerMonth 
                    { 
                        MonthNumber = i, 
                        Number = subscriptions.Where(x => x.PaymentDate.Month == i).Count() 
                    });
            }

            return numberOfUserSubscriptionsPerMonth;
        }

    }
}
