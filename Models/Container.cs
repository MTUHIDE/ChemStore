using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum Units
{
    pound, //0
    gallon,//1
    mg,    //2
    g,     //3
    kg,    //4
    mL,    //5
    L      //6
}

namespace ChemStoreWebApp.Models
{
    public partial class Container
    {
        [Key]
        public long ContainerId { get; set; }
        public int Unit { get; set; }
        public int Amount { get; set; }
        public bool Retired { get; set; }
        public string CasNumber { get; set; }
        public string RoomId { get; set; }
        public int SupervisorId { get; set; }

        // public virtual Chemical CasNumberNavigation { get; set; }
        public virtual Location Room { get; set; }
        public virtual Account Supervisor { get; set; }
    }
}
