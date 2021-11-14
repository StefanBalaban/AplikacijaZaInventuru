using ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Models
{
    public class UsersListResponse
    {
        public List<User> Users { get; set; }
    }
}
