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

        public DisplayContainer(Container container, List<Chemical> chemicals, List<Location> locations,
            List<Account> accounts)
        {
            chem = (from c in chemicals
                    where c.CasNumber == container.CasNumber
                    select c).First();
            loc = (from l in locations
                   where l.RoomId == container.RoomId
                   select l).First();
            supervisor = (from s in accounts
                   where s.AccountId == container.SupervisorId
                   select s).First();
            con = container;
        }
    }
}
