using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Interfaces
{
    public interface IDataAggregator
    {
        double Average();
        double Sum();
    }
}
