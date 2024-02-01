using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class ContainerNEW
    {
        [Key]
        public int ContainerID { get; set; }
        public int LocationID { get; set; }
        public int SupervisorID { get; set; }
        public string Product_Name { get; set; }
        public double Size { get; set; }

        //What is "text"?
        public string Notes { get; set; }
    }
}
