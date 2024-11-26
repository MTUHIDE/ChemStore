using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class StatementPictogramModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public StatementPictogramModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<StatementPictogram> StatementPictogram { get; set; }

        public async Task OnGetAsync()
        {
            StatementPictogram = await _context.StatementPictogram.ToListAsync();
        }
    }
}
