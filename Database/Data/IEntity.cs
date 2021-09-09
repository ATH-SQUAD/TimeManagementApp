using System;
using System.ComponentModel.DataAnnotations;

namespace TimeManagementApp.Database.Data
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
