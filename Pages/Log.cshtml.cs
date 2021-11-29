using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using ChemStoreWebApp.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChemStoreWebApp.Pages
{
    public class LogModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public LogModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }
        //public IList<DisplayContainer> DisplayContainers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchBuilding { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchDetails { get; set; }
        [BindProperty(SupportsGet = true)]
        public string containerID { get; set; }

        public Boolean textEntered()
        {
            return !(string.IsNullOrEmpty(searchBuilding) &&
                string.IsNullOrEmpty(searchUser) &&
                string.IsNullOrEmpty(searchDetails) &&
                string.IsNullOrEmpty(containerID));
        }

        /// <summary>
        /// Runs on every search and returns a list of containers that fit the given search criteria
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            // stores data from database in arrays to limit amount of calls to database at once
            //var containers = _context.History.ToList();
        }
    }
}