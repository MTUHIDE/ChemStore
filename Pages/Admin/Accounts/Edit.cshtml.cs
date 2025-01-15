using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChemStoreWebApp.Pages.Admin.Account
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.ChemstoreContext _context;

        public EditModel(ChemStoreWebApp.Models.ChemstoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        //public Models.User User { get; set; } This was the orginal line (with Account in place of User ofc) but it said something about hiding base model. Is this safe to delete?
        public new Models.User User { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            /*
            // New User model can't have null Id's. Is this safe to delete?
            if (id == null)
            {
                return NotFound();
            }
            */

            User = await _context.User.FirstOrDefaultAsync(m => m.UserID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*
                if (!LocationExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return RedirectToPage("./Index");
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.LocationID == id);
        }
    }
}
