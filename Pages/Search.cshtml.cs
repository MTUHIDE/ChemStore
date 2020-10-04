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

        public int getNumLocations(Chemical chem)
        {
            var containers = from m in _context.Container select m;
            containers = containers.Where(s => s.ChemId.Equals(chem.CasNumber));
            return (from container in containers select container.LocationId).Distinct().Count();
        }

        public Boolean inLocation(Chemical chem, Location loc)
        {
            var containers = from m in _context.Container select m;
            containers = containers.Where(s => s.ChemId.Equals(chem.CasNumber));
            var locations = (from container in containers select container.LocationId);
            //return locations.Where(s => s.Building
            return false;
        }

        public Boolean textEntered()
        {
            return !(string.IsNullOrEmpty(searchEmail) &&
                string.IsNullOrEmpty(searchCAS) &&
                string.IsNullOrEmpty(searchString) &&
                string.IsNullOrEmpty(searchNumLocation) &&
                string.IsNullOrEmpty(searchLocation) &&
                string.IsNullOrEmpty(searchSize));
        }


        public async Task OnGetAsync()
        {
            var chemicals = from m in _context.Chemical select m;
            if (!string.IsNullOrEmpty(searchCAS))
            {
                chemicals = chemicals.Where(s => s.CasNumber.Equals(Int32.Parse(searchCAS)));
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                chemicals = chemicals.Where(s => s.ChemName.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(searchNumLocation))
            {
                //chemicals = chemicals.Where(s => s.numLocations.Equals(Int32.Parse(searchNumLocation)));
            }
            if (!string.IsNullOrEmpty(searchLocation))
            {
                //chemicals = chemicals.Where(s => s.inLocation(searchLocation));
            }
            if (!string.IsNullOrEmpty(searchSize))
            {

            }
            if (!string.IsNullOrEmpty(searchEmail))
            {

            }
            //Chemical = await Task.FromResult(chemicals.ToList());
            Chemical = await chemicals.ToListAsync();
        }
    }
}
