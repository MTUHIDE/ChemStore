using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class RolePermissions
    {
        // Composite primary key, handled in ChemstoreContext
        public int RoleID { get; set; }
        // Composite primary key, handled in ChemstoreContext
        public int LocationID { get; set; }
        // Composite primary key, handled in ChemstoreContext
        public int Permission { get; set; }
        public bool HasPermission { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; init; }

        [ForeignKey("LocationID")]
        public X_Location X_Location { get; init; }
    }
}