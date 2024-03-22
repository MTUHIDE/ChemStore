using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class RolePermissions
    {
        [Key]
        public int RoleID { get; set; }
        [Key]
        public int LocationID { get; set; }
        [Key]
        public int Permission { get; set; }
        public bool HasPermission { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; init; }

        [ForeignKey("LocationID")]
        public X_Location X_Location { get; init; }
    }
}