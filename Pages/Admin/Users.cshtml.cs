using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Developer")]
    public class UserModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public UserModel(ChemstoreContext context)
        {
            _context = context;
        }

        // public IList<User> User { get; set; } // This is orginal line. Hides an inherited model. Is this safe to delete?
        public new IList<User> User { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
