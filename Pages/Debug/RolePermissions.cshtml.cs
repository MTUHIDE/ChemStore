using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class RolePermissionsModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public RolePermissionsModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<RolePermissions> RolePermissions { get; set; }

        public async Task OnGetAsync()
        {
            RolePermissions = await _context.RolePermissions.ToListAsync();
        }
    }
}
