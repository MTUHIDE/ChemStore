using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class OverridePermissions
    {
        //Primary Key (in chemstoreContext)
        public int UserID { get; set; }
        //Primary Key (in chemstoreContext)
        public int LocationID { get; set; }
        //Primary Key (in chemstoreContext)
        public int Permission { get; set; }
        public bool HasPermission { get; set; }

        [ForeignKey("UserID")]
        public User User { get; init; }

        [ForeignKey("LocationID")]
        public X_Location X_Location { get; init; }
    }
}