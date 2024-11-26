using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;
using Microsoft.Build.Framework.Profiler;

namespace ChemStoreWebApp.Pages.Admin.Account
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public CreateModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        // public Models.User User { get; set; } // This was the orginal Line but it hides an inherited model. Is this safe to delete?
        public new Models.User User { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && User.DepartmentID != null) // (... && Account.RoleID != null) was orginally on here since the old Account model roleID was nullable, but User's is not. Is this safe to delete?
            {
                _context.User.Add(User);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
