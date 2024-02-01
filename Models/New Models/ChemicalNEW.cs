using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ChemStoreWebApp.Models
{
    public partial class ChemicalNEW
    {
        [Key]
        public BigInteger ChemicalID { get; set; }
        public int CasNum { get; set; }
        public char HCode { get; set; }

        //What is this? It's just "..." in ER diagram.
        public string whatisthis { get; set; }
    }
}
