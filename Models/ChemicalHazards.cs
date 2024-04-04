using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class ChemicalHazards
    {
        [Key]
        public int IDChemicalHazard { get; set; }
        public string CasNumber { get; set; }
        public string HazardId { get; set; }

        public virtual Chemical CasNumberNavigation { get; set; }
    }
}
