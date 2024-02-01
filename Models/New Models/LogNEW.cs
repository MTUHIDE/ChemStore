using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class LogNEW
    {

        [Key]
        public int IDLog { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserID { get; set; }
        public string Table { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int Action { get; set; }
    }

    public enum Action
    {
        //This was COPIED from old Log, so these will
        //probably have to be updated.
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
        RequestAccepted,
        [Display(Name = "Chemical Added")]
        ChemicalAdded,
        [Display(Name = "Chemical Removed")]
        ChemicalRemoved,
        [Display(Name = "Chemical Transferred")]
        ChemicalTransferred,
        [Display(Name = "Chemical Edited")]
        ChemicalEdited
    }
}
