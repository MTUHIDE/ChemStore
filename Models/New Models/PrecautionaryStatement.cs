using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class PrecautionaryStatement
    {
        [Key]
        public char PCode { get; set; }
        public string Statement { get; set; }
    }
}
