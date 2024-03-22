using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class StatementPictogram
    {
        //Primary Key (in chemstoreContext)
        public char GHCode { get; set; }
        //Primary Key (in chemstoreContext)
        public char HCode { get; set; }

        [ForeignKey("GHCode")]
        public HazardPictogram HazardPictogram { get; init; }

        [ForeignKey("HCode")]
        public HazardStatement HazardStatement { get; init; }
    }
}