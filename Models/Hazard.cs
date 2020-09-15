using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class Hazard
    {
        public Hazard()
        {
            HasHazard = new HashSet<HasHazard>();
        }

        public int HazardId { get; set; }
        public string HazardDetails { get; set; }

        public virtual ICollection<HasHazard> HasHazard { get; set; }
    }
}
