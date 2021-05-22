using TimeManagementApp.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.Database.Models
{
    interface IConnection
    {
        public string DbName { get; set; }
    }
}
