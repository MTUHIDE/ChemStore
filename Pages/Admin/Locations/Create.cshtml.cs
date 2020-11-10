using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Locations
{
    public class CreateModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public CreateModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LocationFid"] = new SelectList(_context.HasLocation, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Location.Add(Location);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
