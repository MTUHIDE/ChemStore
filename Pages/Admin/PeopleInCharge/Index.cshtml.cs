using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.PeopleInCharge
{
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<PersonInCharge> PersonInCharge { get;set; }

        public async Task OnGetAsync()
        {
            PersonInCharge = await _context.PersonInCharge
                .Include(p => p.Pic).ToListAsync();
        }
    }
}
