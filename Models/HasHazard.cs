using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class HasHazard
    {
        public int? HazardId { get; set; }
        public int? ChemicalId { get; set; }
        public int Id { get; set; }

        public virtual Chemical Chemical { get; set; }
        public virtual Hazard Hazard { get; set; }
    }
}
