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
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages
{
    [Authorize(Policy = "Admin")]
    public class LogModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public LogModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }
       
        public List<Log> LogEntries { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchAction { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchRole { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchDetails { get; set; }
        [BindProperty(SupportsGet = true)]
        public string containerID { get; set; }
        [BindProperty(SupportsGet = true)]
        public int sortMethod { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int prevSort { get; set; } = -1;

        public Boolean textEntered()
        {
            return !( string.IsNullOrEmpty(searchUser) &&
                string.IsNullOrEmpty(searchDetails) &&
                string.IsNullOrEmpty(containerID) &&
                string.IsNullOrEmpty(searchAction) &&
                string.IsNullOrEmpty(searchRole));
        }

        public Boolean isValidSearchItem(Log entry, bool ignoreCase)
        {
            var checkCase = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            if (!string.IsNullOrEmpty(searchAction) && !entry.Action.ToString().Equals(searchAction))
                return false;
            //Some variables in the entry can be null. Uses the null conditional operator to break when null
            if (!string.IsNullOrEmpty(searchRole) && (!entry.User?.Role.ToString().Equals(searchRole) ?? true))
                return false;
            if(!string.IsNullOrEmpty(searchUser) && (!entry.User?.Email.Contains(searchUser, checkCase) ?? true))
                return false;
            if (!string.IsNullOrEmpty(searchDetails) && (!entry.Description?.Contains(searchDetails, checkCase) ?? true))
                return false;
            if (!string.IsNullOrEmpty(containerID) && (!entry.ContainerID?.ToString().Contains(containerID) ?? true))
                return false;

            return true;
        }

        /// <summary>
        /// Runs on every search and returns a list of containers that fit the given search criteria
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            var log = _context.Log.ToList();
            var accounts = _context.Account.ToList();

            //Updates the User variable for the Log entry objects
            foreach (var e in log)
            {
                e.User = (from a in accounts
                          where a.AccountId == e.UserID
                          select a).FirstOrDefault();
            }

            //Filter the results if text has been entered in one of the parameters
            if(textEntered() == true)
            {
                LogEntries = await Task.FromResult(
                log.Where(c => isValidSearchItem(c, true))
                .ToList());
            }
            else
            {
                LogEntries = log;
            }

            if (prevSort == sortMethod)
            {
                //Reverses the sort method
                sortMethod++;
            }
            prevSort = sortMethod;

            IOrderedEnumerable<Log> temp = sortMethod switch
            {
                1 => LogEntries.OrderBy(c => c.DateTime),
                //Checks if the entry is null or not and puts the null values at the end
                2 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.User?.Email)).ThenBy(c => c.User?.Email),
                3 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.User?.Email)).ThenByDescending(c => c.User?.Email),
                4 => LogEntries.OrderBy(c => c.Action.HasValue).ThenBy(c => c.Action),
                5 => LogEntries.OrderBy(c => c.Action.HasValue).ThenByDescending(c => c.Action),
                6 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.Description)).ThenBy(c => c.Description),
                7 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.Description)).ThenByDescending(c => c.Description),
                8 => LogEntries.OrderBy(c => c.ContainerID).ThenBy(c => c.ContainerID),
                9 => LogEntries.OrderByDescending(c => c.ContainerID).ThenBy(c => c.ContainerID),

                _ => LogEntries.OrderByDescending(c => c.DateTime),
            };
            
            //Order by time if thats not already the sorted by category
            if(sortMethod != 1 && sortMethod != 0)
            {
                temp = temp.ThenByDescending(c => c.DateTime);
            }

            LogEntries = temp.ToList();
        }
    }
}