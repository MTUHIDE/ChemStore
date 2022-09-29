﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;

namespace ChemStoreWebApp.Pages.Admin.Chemicals
{
    public class DetailsModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public DetailsModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        public Chemical Chemical { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chemical = await _context.Chemical.FirstOrDefaultAsync(m => m.CasNumber == id);

            if (Chemical == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
