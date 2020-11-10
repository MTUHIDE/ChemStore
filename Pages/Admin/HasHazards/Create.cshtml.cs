using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.HasHazards
{
    public class CreateModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public CreateModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ChemicalId"] = new SelectList(_context.Chemical, "CasNumber", "CasNumber");
        ViewData["HazardId"] = new SelectList(_context.Hazard, "HazardId", "HazardId");
            return Page();
        }

        [BindProperty]
        public HasHazard HasHazard { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.HasHazard.Add(HasHazard);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
