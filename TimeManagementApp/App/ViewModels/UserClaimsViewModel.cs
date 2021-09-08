using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.App.Models;

namespace TimeManagementApp.App.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public string RoleId{ get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
