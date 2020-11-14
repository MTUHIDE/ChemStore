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
        public IList<Chemical> Chemical { get; set; }
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

        /// <summary>
        /// Gets the number of locations a chemical is stored in
        /// </summary>
        /// <param name="chem">The specified chemical</param>
        /// <returns>Number of locations it's stored in</returns>
        public int getNumLocations(Chemical chem)
        {
            var containers = from m in _context.Container
                             where m.ChemId == chem.CasNumber
                             select m;
            return (from container in containers select container.LocationId).Distinct().Count();
        }
        /// <summary>
        /// Checks whether a chemical is in any locations matching the <c>loc</c> search string
        /// </summary>
        /// <param name="chem">The specified chemical</param>
        /// <param name="loc">Search string for the name of the location</param>
        /// <returns>True if any location matching <c>loc</c> stores <c>chem</c></returns>
        public Boolean isInLocation(Chemical chem, String loc)
        {
            var containers = from m in _context.Container
                             where m.ChemId == chem.CasNumber
                             select m;
            var buildings = from m in _context.Building
                            where m.BuildingName.Contains(loc)
                            select m;
            return (from c in containers
                    join b in buildings on c.LocationId equals b.BuildingId
                    select new { id = c.LocationId }).Distinct().Count() > 0;
        }
        /// <summary>
        /// Checks whether a chemical is in any containers matching the specified size
        /// </summary>
        /// <param name="chem">The specified chemical</param>
        /// <param name="size">Size of container, without units</param>
        /// <returns>True if any container of size <c>size</c> contains <c>chem</c></returns>
        public Boolean isInSize(Chemical chem, int size)
        {
            return (from c in _context.Container
                    where c.ChemId == chem.CasNumber && c.Size == size
                    select c).Count() > 0;
        }
        /// <summary>
        /// Checks whether a chemical is stored in any containers under a PIC having an email with the search string <c>email</c>
        /// </summary>
        /// <param name="chem">The specified chemical</param>
        /// <param name="email">Disired email address for PIC</param>
        /// <returns>True if any container under a PIC with <c>email</c> stores <c>chem</c></returns>
        public Boolean hasPicEmail(Chemical chem, string email)
        {
            var containers = from m in _context.Container
                             where m.ChemId == chem.CasNumber
                             select m;
            var Pics = from m in _context.PersonInCharge
                       where m.Email.Contains(email)
                       select m;
            return (from c in containers
                    join p in Pics on c.ContainerNavigation.PersonInCharge.PicId equals p.PicId
                    select new { id = p.PicId }).Distinct().Count() > 0;
        }
        /// <summary>
        /// Check if text was input to any of the search fields
        /// </summary>
        /// <returns>True if any search field contains text</returns>
        public Boolean textEntered()
        {
            return !(string.IsNullOrEmpty(searchEmail) &&
                string.IsNullOrEmpty(searchCAS) &&
                string.IsNullOrEmpty(searchString) &&
                string.IsNullOrEmpty(searchNumLocation) &&
                string.IsNullOrEmpty(searchLocation) &&
                string.IsNullOrEmpty(searchSize));
        }
        /// <summary>
        /// Check if <c>chem</c> matches the desired search parameters
        /// </summary>
        /// <param name="chem">The chemical to check against</param>
        /// <returns>True if <c>chem</c> matches the search parameters</returns>
        public Boolean isValidSearchItem(Chemical chem)
        {
            //var result = true;
            if (!string.IsNullOrEmpty(searchCAS) && chem.CasNumber != Int32.Parse(searchCAS)) 
                return false;
            if (!string.IsNullOrEmpty(searchString) && !chem.ChemName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!string.IsNullOrEmpty(searchNumLocation) && getNumLocations(chem) != Int32.Parse(searchNumLocation))
                return false;
            if (!string.IsNullOrEmpty(searchLocation) && !isInLocation(chem, searchLocation))
                return false;
            if (!string.IsNullOrEmpty(searchSize) && !isInSize(chem, Int32.Parse(searchSize)))
                return false;
            if (!string.IsNullOrEmpty(searchEmail) && !hasPicEmail(chem, searchEmail))
                return false;

            return true;
        }

        public async Task OnGetAsync()
        {
            var chemicals = _context.Chemical // gets a list of chemicals that should be included, given a particular search
                .AsEnumerable()
                .Where(s => isValidSearchItem(s));
            Chemical = await Task.FromResult(chemicals.ToList()); // lists the chemicals on the page
        }
    }
}
