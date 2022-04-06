using System;
using System.Collections.Generic;

public enum Units
{
    pound,
    gallon,
    mg,
    g,
    kg,
    mL,
    L    
}

namespace ChemStoreWebApp.Models
{
    public partial class Container
    {
        public long ContainerId { get; set; }
        public int Unit { get; set; }
        public int Amount { get; set; }
        public bool Retired { get; set; }
        public string CasNumber { get; set; }
        public string RoomId { get; set; }
        public int SupervisorId { get; set; }

        public virtual Chemical CasNumberNavigation { get; set; }
        public virtual Location Room { get; set; }
        public virtual Account Supervisor { get; set; }
    }
}
