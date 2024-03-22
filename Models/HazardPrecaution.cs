using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class HazardPrecaution
    {
        //Primary Key (in chemstoreContext)
        public char HCode { get; set; }
        //Primary Key (in chemstoreContext)
        public char PCode { get; set; }

        [ForeignKey("HCode")]
        public HazardStatement HazardStatement { get; init; }
    }
}