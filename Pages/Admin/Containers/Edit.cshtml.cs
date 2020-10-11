using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Containers
{
    public class EditModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public EditModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["ContainerId"] = new SelectList(_context.HasLocation, "Id", "Id");
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

            _context.Attach(Container).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerExists(Container.ContainerId))
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

        private bool ContainerExists(int id)
        {
            return _context.Container.Any(e => e.ContainerId == id);
        }
    }
}
