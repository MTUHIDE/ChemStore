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
    [Authorize(Roles = "Admin")]
    public class LogModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.ChemstoreContext _context;

        public LogModel(ChemStoreWebApp.Models.ChemstoreContext context)
        {
            _context = context;
        }
       
        public List<X_Log> LogEntries { get; set; }

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
        public DateTime? startTime { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? endTime { get; set; }
        [BindProperty(SupportsGet = true)]
        public int prevSort { get; set; } = -1;

        public Boolean textEntered()
        {
            return !( string.IsNullOrEmpty(searchUser) &&
                string.IsNullOrEmpty(searchDetails) &&
                string.IsNullOrEmpty(containerID) &&
                string.IsNullOrEmpty(searchAction) &&
                string.IsNullOrEmpty(searchRole) &&
                dateNullOrEmpty(startTime) &&
                dateNullOrEmpty(endTime));
        }

        public Boolean dateNullOrEmpty(DateTime? date)
        {
            return date?.Equals(DateTime.MinValue) ?? true;
        }

        public IQueryable<X_Log> validSearchItems(IQueryable<X_Log> log, bool ignoreCase)
        {
            var checkCase = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            // I have to use ternary here because null-propogating ops (i.e. entry.User?. etc.) are not supported for this type of call
            // indexOf is also a hack because contains was causing issues
            var logs = from entry in log
                       where (string.IsNullOrEmpty(searchAction)  || entry.Action.ToString().Equals(searchAction)) &&
                             (string.IsNullOrEmpty(searchRole)    || (entry.User == null ? true : entry.User.Role.ToString().Equals(searchRole))) &&
                             //(string.IsNullOrEmpty(searchUser)    || (entry.User == null ? true : EF.Functions.Like(entry.User.Email, "%" + searchUser + "%"))) &&
                             (string.IsNullOrEmpty(searchDetails) || (entry.Notes == null ? true : EF.Functions.Like(entry.Notes, "%" + searchDetails + "%"))) &&
                             //(string.IsNullOrEmpty(containerID)   || (entry.ContainerID == null ? true : EF.Functions.Like(entry.ContainerID.ToString(), "%" + containerID + "%"))) &&
                             (dateNullOrEmpty(startTime) || entry.Timestamp >= startTime) &&
                             (dateNullOrEmpty(endTime) || entry.Timestamp <= endTime)
                       select entry;

            return logs;
        }

        /// <summary>
        /// Runs on every search and returns a list of containers that fit the given search criteria
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            var log = _context.X_Log.AsQueryable();

            await log.ForEachAsync((item) =>
            {
                /* Error below from log to X_Log conversion
                item.User = (from a in _context.Account
                             where a.AccountId == item.UserID
                             select a).FirstOrDefault();
                */
            });

            //Filter the results if text has been entered in one of the parameters
            if(textEntered())
            {
                LogEntries = await validSearchItems(log, true).ToListAsync(); 
            }
            else
            {
                LogEntries = await log.ToListAsync();
            }

            if (prevSort == sortMethod)
            {
                //Reverses the sort method
                sortMethod++;
            }
            prevSort = sortMethod;

            /* Issues occured when switching from log to X_Log
            IOrderedEnumerable<Log> temp = sortMethod switch
            {
                1 => LogEntries.OrderBy(c => c.Timestamp),
                //Checks if the entry is null or not and puts the null values at the end
                //username was email before log to X_log Switch
                2 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.User?.Username)).ThenBy(c => c.User?.Username),
                3 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.User?.Username)).ThenByDescending(c => c.User?.Username),
                4 => LogEntries.OrderBy(c => c.Action.HasValue).ThenBy(c => c.Action),
                5 => LogEntries.OrderBy(c => c.Action.HasValue).ThenByDescending(c => c.Action),
                6 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.Notes)).ThenBy(c => c.Notes),
                7 => LogEntries.OrderBy(c => String.IsNullOrEmpty(c.Notes)).ThenByDescending(c => c.Notes

                _ => LogEntries.OrderByDescending(c => c.Timestamp),
            };
            
            
            //Order by time if thats not already the sorted by category
            if(sortMethod != 1 && sortMethod != 0)
            {
                temp = temp.ThenByDescending(c => c.DateTime);
            }

            LogEntries = temp.ToList();
            */
        }
    }
}