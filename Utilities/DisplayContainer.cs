using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Utilities
{
    public class DisplayContainer
    {
        public Chemical chem { get; set; } // saves the chemical data for the chemical in the container
        public Container con { get; set; } // saves the container data
        public Location loc { get; set; } // saves the location data for the container
        public Account supervisor { get; set; } // saves the account (supervisor) data for the container

        public DisplayContainer(Container container, Chemical chemical, Location location,
            Account accounts)
        {
            chem = chemical;
            loc = location;
            supervisor = accounts;
            con = container;
        }
    }
}
