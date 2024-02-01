using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class StatementPictogram
    {
        [Key]
        public char GHCode { get; set; }
        public char HCode { get; set; }
    }
}
