using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Developer")]
    public class LocationModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public LocationModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<X_Location> Location { get; set; }

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

            X_Location location = await _context.Location.FindAsync(selectedLocation);

            if (location != null)
            {
                _context.Location.Remove(location);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
