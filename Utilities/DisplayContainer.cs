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
        public Building building { get; set; } // saves the building data for the container
        public PersonInCharge pic { get; set; } // saves the person in charge data for the container

        public DisplayContainer(Container container, List<Chemical> chemicals, List<Location> locations,
            List<Building> buildings, List<PersonInCharge> pics)
        {
            chem = (from c in chemicals
                    where c.CasNumber == container.ChemId
                    select c).First();
            loc = (from l in locations
                   where l.LocationId == container.LocationId
                   select l).First();
            building = (from b in buildings
                   where b.BuildingId == container.LocationId
                   select b).First();
            pic = (from p in pics
                   where p.PicId == container.PicId
                   select p).First();
            con = container;
        }
    }
}
