using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class Chemical
    {
        public Chemical()
        {
            ChemicalHazards = new HashSet<ChemicalHazards>();
            Container = new HashSet<Container>();
        }

        public string CasNumber { get; set; }
        public string ChemicalName { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public string CatalogNumber { get; set; }

        public virtual ICollection<ChemicalHazards> ChemicalHazards { get; set; }
        public virtual ICollection<Container> Container { get; set; }
    }
}
