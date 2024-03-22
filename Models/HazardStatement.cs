using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models
{
    public partial class HazardStatement
    {
        [Key]
        public char HCode { get; set; }
        public string Statements { get; set; }
        public string Category { get; set; }
        public int Class { get; set; }
        public int SignalWord { get; set; }
    }

    public enum Class
    {
        [Display(Name = "Class 1")] // 0
        Museum,
        [Display(Name = "Class 2")] // 1
        AOB
    }

    public enum SignalWord
    {
        [Display(Name = "Word 1")] // 0
        Museum,
        [Display(Name = "Word 2")] // 1
        AOB
    }
}