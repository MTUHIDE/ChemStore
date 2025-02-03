using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Admin.Departments
{
    [Authorize(Roles = "Admin, Developer")]
    public class IndexModel : PageModel
    {
        private readonly Models.ChemstoreContext _context;

        public IndexModel(Models.ChemstoreContext context)
        {
            _context = context;
        }

        public IList<Models.Department> Department { get; set; }

        public async Task OnGetAsync()
        {
            Department = await _context.Department.ToListAsync();
        }
    }
}
