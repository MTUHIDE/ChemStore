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
            return Page();
        }

        [BindProperty]
        public Models.X_Location Location { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                int id = Convert.ToInt32(Guid.NewGuid());
                Location.LocationID = id;
                _context.Location.Add(Location);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
