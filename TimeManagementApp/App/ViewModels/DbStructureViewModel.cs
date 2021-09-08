using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class DbStructureViewModel
    {
        public Guid ReportId { get; set; }
        public Dictionary<string,  HashSet<string>> DbStructure{ get; set; }
    }
}
