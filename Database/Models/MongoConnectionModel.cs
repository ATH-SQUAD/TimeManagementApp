using TimeManagementApp.Database.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeManagementApp.Database.Models
{
    public class MongoConnectionModel : IConnectionEntity
    {
        public Guid Id { get; set; }
        public string DbName { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public ApplicationUser User { get; set; }
        public string Hostname { get; set; }
        public string Port { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
