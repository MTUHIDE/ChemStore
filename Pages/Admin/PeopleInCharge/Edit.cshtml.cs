using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.PeopleInCharge
{
    public class EditModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public EditModel(ChemStoreWebApp.Models.chemstoreContext context)
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
           ViewData["PicId"] = new SelectList(_context.HasLocation, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PersonInCharge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonInChargeExists(PersonInCharge.PicId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonInChargeExists(int id)
        {
            return _context.PersonInCharge.Any(e => e.PicId == id);
        }
    }
}
