using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Tables
{
    public class DataTables
    {
        public class AllUsers
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int AutoId { get; set; }
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "email")]
            public string Email { get; set; }
            public bool IsActivated { get; set; }
            public Search Search { get; set; }
            public int Draw { get; set; }
        }

        public class Search
        {
            public string Value { get; set; }
            public bool IsRegex { get; set; }
        }
    }
}
