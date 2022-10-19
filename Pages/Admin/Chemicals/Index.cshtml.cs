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

namespace ChemStoreWebApp.Pages.Admin.Chemicals
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<Chemical> Chemical { get; set; }

        public async Task OnGetAsync()
        {
            Chemical = await _context.Chemical.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(string selectedChemical)
        {
            if (selectedChemical == null)
            {
                return NotFound();
            }

            Chemical chem  = await _context.Chemical.FindAsync(selectedChemical);

            if (chem != null)
            {
                _context.Chemical.Remove(chem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
