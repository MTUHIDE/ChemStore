using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Locations
{
    public class DetailsModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DetailsModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Location
                .Include(l => l.LocationF).FirstOrDefaultAsync(m => m.LocationId == id);

            if (Location == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
