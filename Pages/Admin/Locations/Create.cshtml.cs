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
using Location = ChemStoreWebApp.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;
using Microsoft.Build.Framework.Profiler;

namespace ChemStoreWebApp.Pages.Admin.LocationCRUD
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public CreateModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }
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
