using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ChemStoreWebApp.Models
{
    public partial class ContainerChemicals
    {
        //Primary Key (in chemstoreContext)
        public Int64 ChemicalID { get; set; }
        //Primary Key (in chemstoreContext)
        public int ContainerID { get; set; }
        public double Quantity { get; set; }
        public string? Manufacturer { get; set; }
        public string? CatalogNumber { get; set; }
        public int PreferredUnit { get; set; }
        public int? StateOfMatter { get; set; }

        [ForeignKey("ContainerID")]
        public X_Container X_Container { get; init; }
    }

    public enum PreferredUnit
    {
        [Display(Name = "Unit 1")] // 0
        Museum,
        [Display(Name = "Unit 2")] // 1
        AOB
    }

    public enum StateOfMatter
    {
        [Display(Name = "Solid")] // 0
        Solid,
        [Display(Name = "Liquid")] // 1
        Liquid,
        [Display(Name = "Gas")] // 2
        Gas,
        [Display(Name = "Plasma")] // 3
        Plasma,
        [Display(Name = "Bose-Einstein condensate")] // 4
        BEC
    }
}