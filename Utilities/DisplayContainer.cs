using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Utilities
{
    public class DisplayContainer
    {
        // TODO: Should ContainerChemicals be here? Each Container can have multiple entries in ContainerChemicals
        // public Chemical chem { get; set; } // saves the chemical data for the chemical in the container
        public X_Container con { get; set; } // saves the container data
        public X_Location loc { get; set; } // saves the location data for the container
        public User supervisor { get; set; } // saves the user (supervisor) data for the container. This will probaly need to first be found through location's supervisorID

        public DisplayContainer(X_Container container, /*Chemical chemical,*/ X_Location location /*,
            User user*/)
        {
            //chem = chemical;
            loc = location;
            //supervisor = user;
            con = container;
        }
    }
}
