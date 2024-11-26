using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Debug
{
    [Authorize(Roles = "Developer")]
    public class ContainerChemicalsModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public ContainerChemicalsModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<ContainerChemicals> ContainerChemicals { get; set; }

        public async Task OnGetAsync()
        {
            ContainerChemicals = await _context.ContainerChemicals.ToListAsync();
        }
    }
}
