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
       
        public IList<Log> LogEntries { get; set; }

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

        public Boolean textEntered()
        {
            return !( string.IsNullOrEmpty(searchUser) &&
                string.IsNullOrEmpty(searchDetails) &&
                string.IsNullOrEmpty(containerID) &&
                string.IsNullOrEmpty(searchDetails) &&
                string.IsNullOrEmpty(searchAction) &&
                string.IsNullOrEmpty(searchRole));
        }

        public Boolean isValidSearchItem(Log entry, bool ignoreCase)
        {
            var checkCase = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            if (!string.IsNullOrEmpty(searchAction) && !entry.Action.ToString().Equals(searchAction))
                return false;
            if (!string.IsNullOrEmpty(searchRole) && (!entry.User?.Role?.ToString().Equals(searchRole) ?? true))
                return false;
            /*if (!string.IsNullOrEmpty(searchRole) && !(entry.User == null) && !entry.User?.Role.ToString().Equals(searchRole))
                return false; */
            if (!string.IsNullOrEmpty(containerID) && entry.ContainerID != long.Parse(containerID))
                return false;
            if (!string.IsNullOrEmpty(searchUser) && !(entry.User == null) && !entry.User.Email.Contains(searchUser, checkCase))
                return false;



            if (!string.IsNullOrEmpty(searchUser))
            {
                if(entry.User != null)
                {
                    if(!entry.User.Email.Contains(searchUser, checkCase))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(searchDetails))
            {
                if (entry.Description != null)
                {
                    if (!entry.Description.Contains(searchDetails, checkCase))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

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

            //Updates the User variable for the Log objects
            foreach (var e in log)
            {
                e.User = (from a in accounts
                          where a.AccountId == e.UserID
                          select a).FirstOrDefault();
            }

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
        }
    }
}