using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class HazardPictogramModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public HazardPictogramModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<HazardPictogram> HazardPictogram { get; set; }

        public async Task OnGetAsync()
        {
            HazardPictogram = await _context.HazardPictogram.ToListAsync();
        }
    }
}
