using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.HasLocations
{
    public class DetailsModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DetailsModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public HasLocation HasLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HasLocation = await _context.HasLocation.FirstOrDefaultAsync(m => m.Id == id);

            if (HasLocation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
