using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class ConnectionViewModel
    {
        public MongoConnectionViewModel MongoConnectionViewModel { get; set; }
        public SqlServerConnectionViewModel SqlServerConnectionViewModel { get; set; }
        public string DbType { get; set; }
        public string DbName { get; set; }
    }
}
