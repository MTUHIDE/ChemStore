using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class PrecautionaryStatementModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public PrecautionaryStatementModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<PrecautionaryStatement> PrecautionaryStatement { get; set; }

        public async Task OnGetAsync()
        {
            PrecautionaryStatement = await _context.PrecautionaryStatement.ToListAsync();
        }
    }
}
