﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TimeManagementApp.App.ViewModels
{
    public class EmailConfirmationViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
