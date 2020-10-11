using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Containers
{
    public class DetailsModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DetailsModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public Container Container { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Container = await _context.Container
                .Include(c => c.ContainerNavigation).FirstOrDefaultAsync(m => m.ContainerId == id);

            if (Container == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
