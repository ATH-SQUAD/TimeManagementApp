using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class SqlServerConnectionViewModel
    {
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
