using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Services
{
    public interface IUserService
    {
        Task<int> GetNumberOfUsersAsync();
    }
}
