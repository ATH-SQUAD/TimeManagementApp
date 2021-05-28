using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.DBModels
{
    [Index(nameof(Login))]
    [Table("Person")]
    public class PersonModel
    {
        [BsonId]
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        public string Password;

        [Required]
        public string status;

    }
}
