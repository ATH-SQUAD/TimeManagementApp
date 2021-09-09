using TimeManagementApp.App.Enums;
using TimeManagementApp.Database.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.Database.Models
{
    public class ConnectionModel : IConnectionEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DbType DbType { get; set; }
        public Guid ConnectionId { get; set; }
        public string DbName { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public ApplicationUser User { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
