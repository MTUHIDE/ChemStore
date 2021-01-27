using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChemStoreWebApp.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public SearchModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        // variables bound to the URL storing search terms
        [BindProperty(SupportsGet = true)]
        public string searchEmail { get; set; }
        public IList<Container> Container { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchCAS { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchNumLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchSize { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchUnits { get; set; }

        /// <summary>
        /// Gets the room number of a specified container
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>room number</returns>
        public int getRoomNum(Container con)
        {
            return (int)(from l in _context.Location
                    where l.LocationId == con.LocationId
                    select l.Room).First();
        }

        /// <summary>
        /// Gets the chemical name of a specified container
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>Chemical name</returns>
        public async Task<Chemical> getChem(Container con)
        {
            //return "testing";
            return await Task.FromResult((from c in _context.Chemical
                    where c.CasNumber == con.ChemId
                    select c).First());
        }

        public string getChemName2(Container con)
        {
            //return "testing";
            return (from c in _context.Chemical
                                          where c.CasNumber == con.ChemId
                                          select c.ChemName).First();
        }

        /// <summary>
        /// Gets the hazard ID of a specified container
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>Hazard ID</returns>
        public int getHazardId(Container con)
        {
            return (int)(from c in _context.Chemical
                    where c.CasNumber == con.ChemId
                    select c.HazardId).First();
        }

        /// <summary>
        /// Checks if the specified container stores the desired chemical
        /// </summary>
        /// <param name="con">Container object</param>
        /// <param name="chemName">Name of desired chemical</param>
        /// <param name="exact">If true then only returns if <c>chemName</c> is an exact match, otherwise checks if
        /// chemical name contains <c>chemName</c></param>
        /// <returns>True if the container stores the chemical</returns>
        public Boolean isChemical(Container con, String chemName, Boolean exact)
        {
            if (exact)
            {
                return getChem(con).Result.ChemName.Equals(chemName);
            } else
            {
                return getChem(con).ToString().Contains(chemName);
            }
        }

        /// <summary>
        /// Checks the number of locations a chemical is stored in
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>Number of locations</returns>
        public int getNumLocations(Container con)
        {
            /*var containers = from m in _context.Container
                             where m.ChemId == chem.CasNumber
                             select m;
            return (from container in containers select container.LocationId).Distinct().Count();*/
            // obsolete when displaying containers
            if (con == null)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Checks if a container is in the given location
        /// </summary>
        /// <param name="con">Container object</param>
        /// <param name="loc">Building name to search for</param>
        /// <param name="exact">If true then only returns if <c>loc</c> is an exact match, otherwise checks if
        /// building name contains <c>loc</c></param>
        /// <returns>True if the container is in the location</returns>
        public Boolean isInLocation(Container con, String loc, Boolean exact)
        {
            var buildingName = (from b in _context.Building
                                where b.BuildingId == con.LocationId
                                select b.BuildingName).First();
            
            if (exact)
            {
                return buildingName.Equals(loc);
            } else
            {
                return buildingName.Contains(loc);
            }
        }

        /// <summary>
        /// Checks if a container is under the specified PIC with a given email
        /// </summary>
        /// <param name="con">Container object</param>
        /// <param name="email">PIC email</param>
        /// <param name="exact"><param name="exact">If true then only returns if <c>email</c> is an exact match, otherwise checks if
        /// pic email contains <c>email</c></param></param>
        /// <returns>True if the container is under a PIC with the given email</returns>
        public Boolean hasPicEmail(Container con, string email, Boolean exact)
        {
            var picEmail = (from p in _context.PersonInCharge
                            where con.ContainerNavigation.PersonInCharge.PicId == p.PicId
                            select p.Email).First();
            if (exact)
            {
                return picEmail.Equals(email);
            } else
            {
                return picEmail.Contains(email);
            }
        }

        /// <summary>
        /// Checks if there is text entered in any of the search fields
        /// </summary>
        /// <returns>True if text is entered</returns>
        public Boolean textEntered()
        {
            return !(string.IsNullOrEmpty(searchEmail) &&
                string.IsNullOrEmpty(searchCAS) &&
                string.IsNullOrEmpty(searchString) &&
                string.IsNullOrEmpty(searchNumLocation) &&
                string.IsNullOrEmpty(searchLocation) &&
                string.IsNullOrEmpty(searchSize) &&
                string.IsNullOrEmpty(searchUnits));
        }

        /// <summary>
        /// Checks if a container should be listed with the given search criteria
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>True if container should be listed</returns>
        public async Task<Boolean> isValidSearchItem(Container con)
        {
            //var result = true;
            if (!string.IsNullOrEmpty(searchCAS) && con.ChemId != Int32.Parse(searchCAS))
                return false;
            //if (!string.IsNullOrEmpty(searchString) && !isChemical(con, searchString, false))
            if (!string.IsNullOrEmpty(searchString) && !getChem(con).Result.ChemName.Contains(searchString))
            {
                Chemical chem = await Task.FromResult(getChem(con)).Result;
                if (!chem.ChemName.Contains(searchString))
                    return false;
            }
            if (!string.IsNullOrEmpty(searchNumLocation) && getNumLocations(con) != Int32.Parse(searchNumLocation))
                return false;
            if (!string.IsNullOrEmpty(searchLocation) && !isInLocation(con, searchLocation, false))
                return false;
            if (!string.IsNullOrEmpty(searchSize) && con.Size != Int32.Parse(searchSize))
                return false;
            if (!string.IsNullOrEmpty(searchEmail) && !hasPicEmail(con, searchEmail, false))
                return false;
            //if (!string.IsNullOrEmpty(searchUnits))

            return true;
        }

        public async Task OnGetAsync()
        {
            //var containers = _context.Container
            //    .AsEnumerable()
            //    .Where(s => isValidSearchItem(s).Result);
            //Container = await Task.FromResult(containers.ToList());
            Container = await Task.FromResult(_context.Container
                .AsEnumerable()
                .Where(s => isValidSearchItem(s).Result)
                .ToList());
        }
    }
}
