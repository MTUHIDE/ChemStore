using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class LocationAttribute
    {
        [Key]
        public int LocationID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
