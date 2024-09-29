using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class ContainerHazards
    {
        [Key]
        public int ContainerID { get; set; }

        [ForeignKey(nameof(HazardStatement))]
        public char HCode { get; set; }
        public HazardStatement HazardStatement { get; init; }

        [ForeignKey("ContainerID")]
        public X_Container X_Container { get; init; }
    }
}