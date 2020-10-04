using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.PeopleInCharge
{
    public class DeleteModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DeleteModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonInCharge PersonInCharge { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonInCharge = await _context.PersonInCharge
                .Include(p => p.Pic).FirstOrDefaultAsync(m => m.PicId == id);

            if (PersonInCharge == null)
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

            PersonInCharge = await _context.PersonInCharge.FindAsync(id);

            if (PersonInCharge != null)
            {
                _context.PersonInCharge.Remove(PersonInCharge);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
