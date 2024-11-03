using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class RoleModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public RoleModel(ChemstoreContext context)
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
