using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class DailyTimeViewModel
    {
        public DateTime Date { get; set; }
        public string Job { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}
