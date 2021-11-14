using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUI.Models;

namespace WinUI.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;
        private readonly string endpoint = "user";

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<int> GetNumberOfUsersAsync()
        {
            var listOfUSers = await _apiService.GetAsync<UsersListResponse>($"{endpoint}?pageSize=1000&index=0");

            return listOfUSers.Users.Count;
        }
    }
}
