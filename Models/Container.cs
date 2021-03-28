using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public enum Units
{
    mL,
    L,
    g,
    kg,
    gallon,
    pound
}

namespace ChemStoreWebApp.Models
{
    public partial class Container
    {
        public int ContainerId { get; set; }
        public Units Unit { get; set; }
        public double? Size { get; set; }
        public int? ChemId { get; set; }
        public int? LocationId { get; set; }
        public int? PicId { get; set; }
        public bool? Retired { get; set; }

        public virtual HasLocation ContainerNavigation { get; set; }
    }
}
