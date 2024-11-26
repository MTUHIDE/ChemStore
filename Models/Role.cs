using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemStoreWebApp.Models
{
    public partial class Role
    {
        [Key] //primary key
        public int RoleID { get; set; }
        public string Name { get; set; }

        public ICollection<RolePermissions> RolePermissions { get; } = new List<RolePermissions>();
    }
}