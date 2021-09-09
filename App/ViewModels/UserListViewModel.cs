using TimeManagementApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class UserListViewModel
    {
        public ApplicationUser User { set; get; }
        public List<string> Roles { set; get; }
    }
}
