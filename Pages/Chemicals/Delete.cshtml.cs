using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Chemicals
{
    public class DeleteModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DeleteModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chemical Chemical { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chemical = await _context.Chemical.FirstOrDefaultAsync(m => m.CasNumber == id);

            if (Chemical == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chemical = await _context.Chemical.FindAsync(id);

            if (Chemical != null)
            {
                _context.Chemical.Remove(Chemical);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
