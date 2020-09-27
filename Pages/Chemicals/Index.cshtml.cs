using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChemStoreWebApp.Pages.Chemicals
{
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<Chemical> Chemical { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchCAS { get; set; }

        public async Task OnGetAsync()
        {

            var chemicals = from m in _context.Chemical select m;
            if (!string.IsNullOrEmpty(searchCAS))
            {
                chemicals = chemicals.Where(s => s.CasNumber.Equals(Int32.Parse(searchCAS)));
            } else if (!string.IsNullOrEmpty(searchString))
            {
                chemicals = chemicals.Where(s => s.ChemName.Contains(searchString));
            }

            Chemical = await chemicals.ToListAsync();
        }
    }
}
