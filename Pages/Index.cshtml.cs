using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ChemStoreWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string chemName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string chemCAS { get; set; }
        [BindProperty(SupportsGet = true)]
        public string location { get; set; }
        [BindProperty(SupportsGet = true)]
        public string numLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public string size { get; set; }

        public void OnGet()
        {
        
        }
        public IActionResult OnPost()
        {
            return new RedirectToPageResult("Search", new {
                searchEmail = email,
                searchCAS = chemCAS, 
                searchString = chemName,
                searchLocation = location,
                searchNumLocation = numLocation,
                searchSize = size,
            });
            
        }
    }
}
