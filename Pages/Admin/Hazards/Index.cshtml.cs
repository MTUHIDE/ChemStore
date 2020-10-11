using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Hazards
{
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<Hazard> Hazard { get;set; }

        public async Task OnGetAsync()
        {
            Hazard = await _context.Hazard.ToListAsync();
        }
    }
}
