using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class Container
    {
        public int ContainerId { get; set; }
        public string Unit { get; set; }
        public double? Size { get; set; }
        public int? ChemId { get; set; }
        public int? LocationId { get; set; }
        public int? PicId { get; set; }

        public virtual HasLocation ContainerNavigation { get; set; }
    }
}
