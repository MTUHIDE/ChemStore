using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.HasHazards
{
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<HasHazard> HasHazard { get;set; }

        public async Task OnGetAsync()
        {
            HasHazard = await _context.HasHazard
                .Include(h => h.Chemical)
                .Include(h => h.Hazard).ToListAsync();
        }
    }
}
