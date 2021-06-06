using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.Database.Data
{
    public interface IConnectionEntity : IEntity
    {
        public string DbName { get; set; }
        public string CreatorId { get; set; }
    }
}
