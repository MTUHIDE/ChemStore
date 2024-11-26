using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class HazardPrecaution
    {
        // Composite primary key, handled in ChemstoreContext
        public char HCode { get; set; }
        // Composite primary key, handled in ChemstoreContext
        public char PCode { get; set; }

        [ForeignKey("HCode")]
        public HazardStatement HazardStatement { get; init; }
    }
}