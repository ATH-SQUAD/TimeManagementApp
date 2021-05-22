using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class MongoConnectionViewModel
    {
        public string Hostname { get; set; }
        public string Port { get; set; }
    }
}
