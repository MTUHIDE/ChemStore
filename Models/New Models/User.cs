using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public int Department { get; set; }
    }

    public enum Role
    {
        [Display(Name = "Role 1")] // 0
        Museum,
        [Display(Name = "Role 2")] // 1
        AOB
    }

    public enum Department
    {
        [Display(Name = "Department 1")] // 0
        Museum,
        [Display(Name = "Department 2")] // 1
        AOB
    }
}
