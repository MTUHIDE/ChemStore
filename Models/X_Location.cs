using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ChemStoreWebApp.Models
{


    public partial class X_Location
    {
        [Key]
        public int LocationID { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }

        [ForeignKey(nameof(X_Location_2))]
        public int ParentID { get; set; }
        public X_Location X_Location_2 { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        public Department Department { get; init; }

        /*[ForeignKey(nameof(User))]
        public int SupervisorID { get; set; }
        public User User { get; init; }*/
    }
}