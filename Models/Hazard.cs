using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class Hazard
    {
        public Hazard()
        {
            ChemicalHazards = new HashSet<ChemicalHazards>();
        }

        public string HazardId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ChemicalHazards> ChemicalHazards { get; set; }
    }
}
