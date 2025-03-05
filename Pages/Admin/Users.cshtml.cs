using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Developer")]
    public class UserModel : PageModel
    {
        private readonly ChemstoreContext _context;

        public UserModel(ChemstoreContext context)
        {
            _context = context;
        }

        // public IList<User> User { get; set; } // This is orginal line. Hides an inherited model. Is this safe to delete?
        public new IList<User> User { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                // Get list of user ids for users who are checked

                // Select users who are checked using user id list
                // User user = (from c in _context.User
                //             where ...
                //             select c);

                // Set user's properties to updated values
                // (i.e. using "user.name = newName" syntax)

                // Save changes to the database
                // _context.SaveChanges();
            }
            catch
            {
                // Handle error?
            }

            // Refresh the page???
            return RedirectToPage();
        }
    }
}
