using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.App.DBModels;

namespace TimeManagementApp.App.DbModel
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TaskId;

        [Required]
        public DateTime TaskStart { get; set; }

        [Required]
        public DateTime TastEnd { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public int PersonId { get; set; }
        public PersonModel PersonModel { get; set; }
    }
}
