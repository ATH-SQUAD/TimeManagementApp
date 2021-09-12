using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class VacationTimeViewModel
    {
        public string Person { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Reason { get; set; }
        public string TotalDays { get; set; }
        public double CalculateDays(DateTime DateFrom, DateTime DateTo)
        {
            return (DateTo - DateFrom).TotalDays;
        }
    }
}
