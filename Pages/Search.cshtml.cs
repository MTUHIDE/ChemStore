using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using ChemStoreWebApp.Utilities;
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

        //public IList<Container> Container { get; set; }
        public IList<DisplayContainer> DisplayContainers { get; set; }

        // variables bound to the URL storing search terms
        [BindProperty(SupportsGet = true)]
        public string searchEmail { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchCAS { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchBuilding { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchSize { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchUnits { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchDepartment { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchRetired { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<int> chemicalsToDelete { get; set; }

        /// <summary>
        /// Checks if there is text entered in any of the search fields
        /// </summary>
        /// <returns>True if text is entered</returns>
        public Boolean textEntered()
        {
            return !(string.IsNullOrEmpty(searchEmail) &&
                string.IsNullOrEmpty(searchCAS) &&
                string.IsNullOrEmpty(searchString) &&
                string.IsNullOrEmpty(searchBuilding) &&
                string.IsNullOrEmpty(searchSize) &&
                string.IsNullOrEmpty(searchUnits) &&
                string.IsNullOrEmpty(searchDepartment));
        }



        public List<int> deleteFromDatabase(List<int> indexes)
        {
            foreach (int containerId in indexes)
            {
                ChemStoreWebApp.Models.Container container = _context.Container.Find(containerId);

                if (container != null)
                {
                    _context.Container.Remove(container);
                    _context.SaveChanges();
                }
            }
            return indexes;
        }

        /// <summary>
        /// Checks if a container should be listed with the given search criteria
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>True if container should be listed</returns>
        public Boolean isValidSearchItem(DisplayContainer con)
        {
            if (!string.IsNullOrEmpty(searchCAS) && con.chem.CasNumber != Int32.Parse(searchCAS))
                return false;
            if (!string.IsNullOrEmpty(searchString) && !con.chem.ChemName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!string.IsNullOrEmpty(searchBuilding) && !con.building.BuildingName.ToString().Equals(searchBuilding))
                return false;
            if (!string.IsNullOrEmpty(searchSize) && con.con.Size != Int32.Parse(searchSize))
                return false;
            if (!string.IsNullOrEmpty(searchEmail) && !con.pic.Email.Contains(searchEmail, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!string.IsNullOrEmpty(searchUnits) && !con.con.Unit.Equals((Units)Int32.Parse(searchUnits)))
                return false;
            if (!string.IsNullOrEmpty(searchDepartment) && !con.loc.Department.ToString().Equals(searchDepartment))
                return false;
            if (!string.IsNullOrEmpty(searchRetired) && bool.Parse(searchRetired) != con.con.Retired)
                return false;

            return true;
        }

        public async Task<IActionResult> OnPostDelete()
        {
            deleteFromDatabase(chemicalsToDelete);
            return RedirectToPage();
        }

        public async Task OnGetAsync()
        {
            // stores data from database in arrays to limit amount of calls to database at once
            var containers = _context.Container.ToList();
            var chemicals = _context.Chemical.ToList();
            var locations = _context.Location.ToList();
            var buildings = _context.Building.ToList();
            var pics = _context.PersonInCharge.ToList();

            DisplayContainers = await Task.FromResult(containers.Select( // creates a DisplayContainer object with all info needed to display it
                c => new DisplayContainer(c, chemicals, locations, buildings, pics))
                .Where(c => isValidSearchItem(c))
                .ToList());
        }
    }
}