using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class LocationAttribute
    {
        //Primary Key (in chemstoreContext)
        public int LocationID { get; set; }
        //Primary Key (in chemstoreContext)
        public string Key { get; set; }
        public string Value { get; set; }

        [ForeignKey("LocationID")]
        public X_Location X_Location { get; init; }
    }
}