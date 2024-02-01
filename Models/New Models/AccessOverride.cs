using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class AccessOverride
    {
        [Key]
        public int UserID { get; set; }
        public int LocationID { get; set; }
        public bool HasAccess { get; set; }
    }
}
