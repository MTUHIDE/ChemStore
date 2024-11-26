using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class HazardPrecautionModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public HazardPrecautionModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<HazardPrecaution> HazardPrecaution { get; set; }

        public async Task OnGetAsync()
        {
            HazardPrecaution = await _context.HazardPrecaution.ToListAsync();
        }
    }
}
