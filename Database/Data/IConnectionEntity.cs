using TimeManagementApp.Database.Data;
using System;

namespace TimeManagementApp.Database.Models
{
    public interface IConnectionEntity : IEntity
    {
        public string DbName { get; set; }
        public string CreatorId { get; set; }
    }
}
