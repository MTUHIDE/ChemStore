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
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public EditModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Account.FirstOrDefaultAsync(m => m.AccountId == id);

            if (Account == null)
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

            _context.Attach(Account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*
                if (!LocationExists(Account.AccountId))
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

        private bool LocationExists(string id)
        {
            return _context.Location.Any(e => e.RoomId == id);
        }
    }
}
