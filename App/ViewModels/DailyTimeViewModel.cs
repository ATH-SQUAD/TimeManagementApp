using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class DailyTimeViewModel
    {
        public string Person { get; set; }
        public DateTime Date { get; set; }
        public string Job { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public double WorkTime { get; set; }

        public double CalculateWorkTime(TimeSpan From, TimeSpan To)
        {
            return To.Hours - From.Hours;
        }
    }
}
