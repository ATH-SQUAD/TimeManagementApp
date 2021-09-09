using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "UserName")]
        public string UserName
        {
            get; set;
        }
    }
}
