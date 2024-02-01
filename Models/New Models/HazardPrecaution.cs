using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class HazardPrecaution
    {
        [Key]
        public char HCode { get; set; }
        public char PCode { get; set; }
    }
}
