using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Models
{
    public class DataTimeLimitation
    {
        public DataTimeLimitation(DateTime timeFrom, DateTime timeTo)
        {
            TimeFrom = timeFrom;
            TimeTo = timeTo;
        }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public TimeSpan TimeIntervalSum
        {
            get
            {
                return TimeTo - TimeFrom;
            }
        }
    }
}
