using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinUI.Models;

namespace WinUI.Services
{
    public interface IUserSubscriptionService
    {
        Task<List<UserSubscription>> GetUserSubscriptionsAsync();

        Task<List<NumberOfUserSubscriptionsPerMonth>> GetProfitForUserSubscriptionsForYear(int year);
    }
}