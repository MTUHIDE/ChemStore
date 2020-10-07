using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChemStoreWebApp.Pages
{
    [Authorize(Roles = "Administrator")]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
