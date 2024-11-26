using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class ContainerModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public ContainerModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<X_Container> X_Container { get; set; }

        public async Task OnGetAsync()
        {
            X_Container = await _context.X_Container.ToListAsync();
        }
    }
}
