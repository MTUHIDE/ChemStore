using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class Chemical
    {
        public Chemical()
        {
            HasHazard = new HashSet<HasHazard>();
        }

        public int CasNumber { get; set; }
        public string ChemName { get; set; }
        public int? HazardId { get; set; }

        public virtual ICollection<HasHazard> HasHazard { get; set; }
    }
}
