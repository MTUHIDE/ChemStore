using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
    }
}