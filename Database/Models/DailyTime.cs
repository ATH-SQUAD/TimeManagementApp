using System;
using TimeManagementApp.Database.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeManagementApp.Database.Models
{
    public class DailyTime : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Person { get; set; }
        public string Date { get; set; }
        public string Job { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string WorkTime { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
