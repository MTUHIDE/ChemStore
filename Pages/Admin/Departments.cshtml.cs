using ChemStoreWebApp.Models;
using ChemStoreWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Developer")]
    public class DepartmentsModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public DepartmentsModel(ChemstoreContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get; set; }

        public async Task OnGetAsync()
        {
            Department = await _context.Department.ToListAsync();
        }

    }
}
