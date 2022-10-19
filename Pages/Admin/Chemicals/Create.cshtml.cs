using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Admin.Chemicals
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
        public Chemical Chemical { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chemical.Add(Chemical);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
