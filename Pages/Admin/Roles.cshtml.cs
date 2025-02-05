using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Developer")]
    public class RolesModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public RolesModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<Role> Role { get; set; }

        public async Task OnGetAsync()
        {
            Role = await _context.Role.ToListAsync();
        }
    }
}
