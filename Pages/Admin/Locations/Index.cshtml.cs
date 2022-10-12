using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChemStoreWebApp.Pages.Admin.Location
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<Models.Location> Location { get;set; }

        public async Task OnGetAsync()
        {
            Location = await _context.Location.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(string selectedLocation)
        {
            if (selectedLocation == null)
            {
                return NotFound();
            }

            Models.Location location = await _context.Location.FindAsync(selectedLocation);

            if (location != null)
            {
                _context.Location.Remove(location);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
