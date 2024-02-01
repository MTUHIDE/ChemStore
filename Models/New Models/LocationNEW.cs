using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ChemStoreWebApp.Models
{


    public partial class LocationNEW
    {
        [Key]
        public int LocationID { get; set; }
        public int ParentID { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int Type { get; set; }
    }

    //What is "type" supposed to represent?
    public enum Type
    {
        [Display(Name = "Type 1")] // 0
        Museum,
        [Display(Name = "Type 2")] // 1
        AOB,
    }
}
