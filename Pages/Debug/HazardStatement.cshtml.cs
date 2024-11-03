using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class HazardStatementModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public HazardStatementModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<HazardStatement> HazardStatement { get; set; }

        public async Task OnGetAsync()
        {
            HazardStatement = await _context.HazardStatement.ToListAsync();
        }
    }
}
