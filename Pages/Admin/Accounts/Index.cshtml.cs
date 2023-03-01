using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChemStoreWebApp.Pages.Admin.Account
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public IList<Models.Account> Account { get;set; }

        public async Task OnGetAsync()
        {
            Account = await _context.Account.ToListAsync();
        }
    }
}
