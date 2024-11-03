using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class LocationModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public LocationModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<X_Location> X_Location { get; set; }

        public async Task OnGetAsync()
        {
            X_Location = await _context.X_Location.ToListAsync();
        }
    }
}
