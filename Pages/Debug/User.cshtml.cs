using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class UserModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public UserModel(ChemstoreContext context)
        {
            _context = context;
        }

        public new IList<User> User { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
