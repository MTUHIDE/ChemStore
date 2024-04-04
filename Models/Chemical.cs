using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class Chemical
    {
        public Chemical()
        {
            Container = new HashSet<Container>();
            // ChemicalHazards = new HashSet<ChemicalHazards>();
        }

        [Key]
        public string CasNumber { get; set; }
        public string ChemicalName { get; set; }
        public string Manufacturer { get; set; }
        public string CatalogNumber { get; set; }

        public virtual ICollection<Container> Container { get; set; }
        // public virtual ICollection<ChemicalHazards> ChemicalHazards { get; set; }
    }
}
