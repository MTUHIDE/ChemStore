using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.ChemInContainers
{
    public class DetailsModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DetailsModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public ChemInContainer ChemInContainer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChemInContainer = await _context.ChemInContainer.FirstOrDefaultAsync(m => m.Id == id);

            if (ChemInContainer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
