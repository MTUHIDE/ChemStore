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
        private readonly ChemStoreWebApp.Models.ChemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.ChemstoreContext context)
        {
            _context = context;
        }

        // public IList<Models.User> User { get;set; } // This is orginal line. Hides an inherited model. Is this safe to delete?
        public new IList<Models.User> User { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
