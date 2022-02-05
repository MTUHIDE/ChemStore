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
    public class SearchModel : PageModel
    {
        private static readonly Dictionary<int, int> orderedUnits = new Dictionary<int, int>
        {
            {(int)Units.L,7},
            {(int)Units.mL,6},
            {(int)Units.kg,5},
            {(int)Units.g,4},
            {(int)Units.mg,3},
            {(int)Units.gallon,2},
            {(int)Units.pound,1}
        }; 

        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public SearchModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }
        public IList<DisplayContainer> DisplayContainers { get; set; }

        // variables bound to the URL storing search terms
        [BindProperty(SupportsGet = true)]
        public string searchEmail { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchCAS { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchBuilding { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchSize { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchUnits { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchDepartment { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchRetired { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<long> chemicalsToDelete { get; set; }
        [BindProperty(SupportsGet = true)]
        public int buildingIndex { get; set; }
        [BindProperty(SupportsGet = true)]
        public string RoomIndex { get; set; }
        [BindProperty(SupportsGet = true)]
        public int buildingEditIndex { get; set; }
        [BindProperty(SupportsGet = true)]
        public string RoomEditIndex { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool createError { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public int containerListIndex { get; set; }

        /// <summary>
        /// Checks if there is text entered in any of the search fields
        /// </summary>
        /// <returns>True if text is entered</returns>
        public Boolean textEntered()
        {
            return !(string.IsNullOrEmpty(searchEmail) &&
                string.IsNullOrEmpty(searchCAS) &&
                string.IsNullOrEmpty(searchString) &&
                string.IsNullOrEmpty(searchBuilding) &&
                string.IsNullOrEmpty(searchSize) &&
                string.IsNullOrEmpty(searchUnits) &&
                string.IsNullOrEmpty(searchDepartment));
        }


        //Deletes containers from the database using a list of their ids
        public List<long> deleteFromDatabase(List<long> containerIds)
        {
            foreach (long id in containerIds)
            {
                //Finds the container associated with the given id and deletes it
                ChemStoreWebApp.Models.Container container = _context.Container.Find(id);

                if (container != null)
                {
                    _context.Container.Remove(container);
                    _context.SaveChanges();
                }
            }
            return containerIds;
        }

        public long addToDatabase(Container con)
        {
            _context.Container.Add(con);
            _context.SaveChanges();
            createError = false;
            return con.ContainerId;
        }

        /// <summary>
        /// Checks if a container should be listed with the given search criteria
        /// </summary>
        /// <param name="con">Container object</param>
        /// <returns>True if container should be listed</returns>
        public Boolean isValidSearchItem(DisplayContainer con, bool ignoreCase)
        {
            var checkCase = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            if (!string.IsNullOrEmpty(searchCAS) && !con.chem.CasNumber.Contains(searchCAS, checkCase))
                return false;
            if (!string.IsNullOrEmpty(searchString) && !con.chem.ChemicalName.Contains(searchString, checkCase))
                return false;
            if (!string.IsNullOrEmpty(searchBuilding) && !con.loc.BuildingName.ToString().Equals(searchBuilding))
                return false;
            if (!string.IsNullOrEmpty(searchSize) && con.con.Amount != Int32.Parse(searchSize))
                return false;
            if (!string.IsNullOrEmpty(searchEmail) && !con.supervisor.Email.Contains(searchEmail, checkCase))
                return false;
            if (!string.IsNullOrEmpty(searchUnits) && !con.con.Unit.Equals((Units)Int32.Parse(searchUnits)))
                return false;
            if (!string.IsNullOrEmpty(searchDepartment) && !con.supervisor.Department.ToString().Equals(searchDepartment))
                return false;

            return true;
        }

        //Deletes selected chemicals on delete button form submission
        public async Task<IActionResult> OnPostDelete()
        {
            deleteFromDatabase(chemicalsToDelete);
            return RedirectToPage();
        }

        // Edits the selected chemical to have the new values as specified in the edit modal
        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                Container con = (from c in _context.Container
                                 where c.ContainerId == Int32.Parse(Request.Form["ContainerID"])
                                 select c).Single();
                con.CasNumber = Request.Form["Cas Number"];
                con.SupervisorId = (from s in _context.Account
                                    where s.Name.Equals(Request.Form["Supervisor"], StringComparison.OrdinalIgnoreCase)
                                    select s.AccountId).Single();
                con.Amount = Int32.Parse(Request.Form["Amount"]);
                con.RoomId = (from l in _context.Location
                              where l.BuildingName == buildingEditIndex && l.RoomNumber == RoomEditIndex
                              select l.RoomId).Single();
                _context.SaveChanges();
            }
            catch
            {
                createError = true;
            }
            
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostCreate()
        {
            Container newCon = new Container();
            newCon.CasNumber = Request.Form["CAS Number"];
            newCon.Unit = 0;
            newCon.Retired = false;
            var roomName = RoomIndex;
            var buildingInt = buildingIndex;
            var supervisorName = Request.Form["Supervisor"];
            try
            {
                var location = _context.Location.Single(x => x.BuildingName == buildingInt && x.RoomNumber == roomName);
                newCon.RoomId = location.RoomId;
                var supervisor = _context.Account.FirstOrDefault(x => x.Name == supervisorName);
                newCon.SupervisorId = supervisor.AccountId;
                newCon.Amount = Convert.ToInt32(Request.Form["Amount"]);
                addToDatabase(newCon);
            }
            catch
            {
                createError = true;
            }
            return RedirectToPage();
        }

        public IEnumerable<Location> GetSubCategories(int? buildingIndex)
        {
            var subCategories = _context.Location.Select(x =>
                new Location { BuildingName = x.BuildingName, RoomId = x.RoomId, RoomNumber = x.RoomNumber });
            return subCategories.Where(s => s.BuildingName == buildingIndex);
        }
        public JsonResult OnGetSubCategories()
        {
            return new JsonResult(GetSubCategories(buildingIndex));
        }

        public JsonResult OnGetEditCategories()
        {
            return new JsonResult(GetSubCategories(buildingEditIndex));
        }

        /// <summary>
        /// Runs on every search and returns a list of containers that fit the given search criteria
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            // stores data from database in arrays to limit amount of calls to database at once
            var containers = _context.Container.ToList();
            var chemicals = _context.Chemical.ToList();
            var accounts = _context.Account.ToList();
            var locations = _context.Location.ToList();

            DisplayContainers = await Task.FromResult(containers.Select( // creates a DisplayContainer object with all info needed to display it
                c => new DisplayContainer(c, chemicals, locations, accounts))
                .Where(c => isValidSearchItem(c, true))
                .ToList());
            sortBySize();
        }

        /*
         public class CaseInsensitiveComparer : IComparer<string> {
         public int Compare(string x, string y) {
         return string.Compare(x, y, true);
          }
            }
         */

        public void sort()
        {
            IOrderedEnumerable<DisplayContainer> temp;
            temp = DisplayContainers.OrderBy(c => c.con.CasNumber);
            //temp.ThenBy
            DisplayContainers = temp.ToList();
        }
        public void sortByCas()
        {
            DisplayContainers = DisplayContainers.OrderBy(c => c.con.CasNumber).ToList();
        }
        public void sortByChemName()
        {
            DisplayContainers = DisplayContainers.OrderBy(c => c.chem.ChemicalName).ToList();
        }
        public void sortBySize()
        {
            DisplayContainers = DisplayContainers.OrderBy(c => c.chem.ChemicalName).ThenByDescending(c => orderedUnits[c.con.Unit]).ThenBy(c => c.con.Amount).ToList();
        }
    }
}