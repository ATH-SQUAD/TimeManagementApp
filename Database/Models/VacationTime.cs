using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.Database.Data;

namespace TimeManagementApp.Database.Models
{
    public class VacationTime : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Person { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Reason { get; set; }
        public string TotalDays { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
