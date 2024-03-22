using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class X_Chemical
    {
        [Key]   //Primary Key
        public Int64 ChemicalID { get; set; }
        public int Cas_Num { get; set; }

        [ForeignKey(nameof(HazardStatement))]   //Foreign Key
        public char H_Code { get; set; }
        public HazardStatement HazardStatement { get; init; }

        //What is this? It's just "..." in ER diagram.
    }
}