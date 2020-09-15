using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class HasLocation
    {
        public HasLocation()
        {
            Location = new HashSet<Location>();
        }

        public int Id { get; set; }

        public virtual Container Container { get; set; }
        public virtual PersonInCharge PersonInCharge { get; set; }
        public virtual ICollection<Location> Location { get; set; }
    }
}
