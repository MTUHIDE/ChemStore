﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class HazardPictogram
    {
        [Key]
        public char GHCode { get; set; }
        public string Description { get; set; }
        public string Pictogram { get; set; } //varchar for image address (switched from Blob [unsupported type])
    }
}