using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class ContainerHazardsModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public ContainerHazardsModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<ContainerHazards> ContainerHazards { get; set; }

        public async Task OnGetAsync()
        {
            ContainerHazards = await _context.ContainerHazards.ToListAsync();
        }
    }
}
