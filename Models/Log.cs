using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



public enum Actions
{
    [Display(Name = "Container Added")]
    ContainerAdded,
    [Display(Name = "Container Removed")]
    ContainerRemoved,
    [Display(Name = "Container Transferred")]
    ContainerTransferred,
    [Display(Name = "Container Edited")]
    ContainerEdited,
    [Display(Name = "User Promoted")]
    UserPromoted,
    [Display(Name = "User Demoted")]
    UserDemoted,
    [Display(Name = "Container Requested")]
    ContainerRequested,
    [Display(Name = "Request Accepted")]
    RequestAccepted
}

namespace ChemStoreWebApp.Models
{
    public partial class Log
    {
        public Log()
        {
        }

        [Key]
        public int IDLog { get; set; }

        public DateTime DateTime { get; set; }
        public int? Action { get; set; }
        public int? UserID { get; set; }
        //This is very intentionally not a foreign key, since that would cause FK conflicts on delete
        public long? ContainerID { get; set; }
        public string Description { get; set; }

        public virtual Account User { get; set; }


    }
}
