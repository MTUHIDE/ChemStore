using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public int? Department { get; set; }
        public int? Room { get; set; }
        public int? Building { get; set; }
        public int LocationFid { get; set; }

        public virtual HasLocation LocationF { get; set; }
    }
}
