using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        [ForeignKey("RoleID")]
        public int RoleID { get; set; }
        public Role Role { get; set; }

        [ForeignKey("DepartmentID")]
        public int? DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}