using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Buildings
{
    public class DeleteModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DeleteModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Building Building { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Building = await _context.Building.FirstOrDefaultAsync(m => m.BuildingId == id);

            if (Building == null)
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

            Building = await _context.Building.FindAsync(id);

            if (Building != null)
            {
                _context.Building.Remove(Building);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
