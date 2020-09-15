using System;
using System.Collections.Generic;

namespace ChemStoreWebApp.Models
{
    public partial class PersonInCharge
    {
        public string Email { get; set; }
        public string PicName { get; set; }
        public int PicId { get; set; }

        public virtual HasLocation Pic { get; set; }
    }
}
