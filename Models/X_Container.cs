using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class X_Container
    {
        [Key]
        public int ContainerID { get; set; }
        public string Product_Name { get; set; }
        public double Size { get; set; }
        public string Notes { get; set; } //What is "text" on ER diagram?

        [ForeignKey(nameof(X_Location))]
        public int LocationID { get; set; }
        public X_Location X_Location { get; init; }
    }
}