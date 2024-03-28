using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;
using Microsoft.Build.Framework.Profiler;

namespace ChemStoreWebApp.Pages.Admin.Location
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly chemstoreContext _context;

        public CreateModel(chemstoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            int[] cids = PUG.REST.GetCIDListAsync("gold").Result; // I believe this causes the system to wait for the result from GetCIDListAsync, change the search term to try other compounds rn

            // Check if the search term existed
            if (cids[0] == -1) // Returned error from PUG.REST.GetCIDListAsync. What to do on error?
            {
                return Page();
            }

            PUG.View.GetChemicalAsync(cids[0]);

            return Page();
        }

        [BindProperty]
        public Models.Location Location { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                String id = Guid.NewGuid().ToString();
                Location.RoomId = id;
                _context.Location.Add(Location);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
