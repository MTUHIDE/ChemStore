using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class ChemicalHazards
    {
        public string CasNumber { get; set; }
        public string HazardId { get; set; }

        public virtual Chemical CasNumberNavigation { get; set; }
        public virtual Hazard Hazard { get; set; }
    }
}
