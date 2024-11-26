using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class LocationAttributeModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public LocationAttributeModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<LocationAttribute> LocationAttribute { get; set; }

        public async Task OnGetAsync()
        {
            LocationAttribute = await _context.LocationAttribute.ToListAsync();
        }
    }
}
