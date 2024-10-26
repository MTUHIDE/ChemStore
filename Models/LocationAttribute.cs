using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class LocationAttribute
    {
        // Composite primary key, handled in ChemstoreContext
        public int LocationID { get; set; }
        // Composite primary key, handled in ChemstoreContext
        public string Key { get; set; }
        public string Value { get; set; }

        [ForeignKey("LocationID")]
        public X_Location X_Location { get; init; }
    }
}