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
using System.ComponentModel;
using ChemStoreWebApp.ViewModels;

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
        }; // Dictionary to sort by units

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
        [BindProperty(SupportsGet = true)]
        public int sortMethod { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public bool reverseNumbers { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool reverseOrder { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public int prevSort { get; set; } = -1;
        [BindProperty(SupportsGet = true)]
        public bool revNums { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool revNumsPrev { get; set; } = false;
        
        [BindProperty(SupportsGet = true)]
        public int numContainers { get; set; }
        [BindProperty(SupportsGet = true)]
        public int uniqueBuildings { get; set; }
        [BindProperty(SupportsGet = true)]
        public int liquidAmount { get; set; }
        [BindProperty(SupportsGet = true)]
        public Units liquidUnit { get; set; }
        [BindProperty(SupportsGet = true)]
        public int solidAmount { get; set; }
        [BindProperty(SupportsGet = true)]
        public Units solidUnit { get; set; }
        [BindProperty(SupportsGet = true)]
        public int pounds { get; set; }
        [BindProperty(SupportsGet = true)]
        public int gallons { get; set; }

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

        public long addToDatabase(Models.Container con)
        {
            _context.Container.Add(con);
            _context.SaveChanges();
            createError = false;
            return con.ContainerId;
        }

        private int getAmount(Units unit)
        {
            return (from con in DisplayContainers where con.con.Unit == (int)unit select con.con.Amount).Sum();
        }

        //Condenses Units into a smaller unit if necessary
        private (int, Units) unitCondenser(int amount, Units unit)
        {
            switch (unit)
            {
                case (Units.L):
                    amount *= 1000;
                    unit = Units.mL;
                    break;
                case (Units.kg):
                    amount *= 1000000;
                    unit = Units.mg;
                    break;
                case (Units.g):
                    amount *= 1000;
                    unit = Units.mg;
                    break;
            }

            if(unit == Units.mL)
            {
                if (amount > 10000)
                {
                    return (amount / 1000, Units.L);
                }
                return (amount, Units.mL);
            }
            else if(unit == Units.mg)
            {
                if(amount > 10000000)
                {
                    return (amount / 1000000, Units.kg);
                }
                else if(amount > 10000)
                {
                    return (amount / 1000, Units.g);
                }
                return (amount, Units.mg);
            }
            return (0, Units.mL);
        }

        private void updateQuickReference()
        {
            liquidAmount = getAmount(Units.L) * 1000 + getAmount(Units.mL);
            (liquidAmount, liquidUnit) = unitCondenser(liquidAmount, Units.mL);
            solidAmount = getAmount(Units.kg) * 1000000 + getAmount(Units.g) * 1000 + getAmount(Units.mg); 
            (solidAmount, solidUnit) = unitCondenser(solidAmount, Units.mg);
            pounds = getAmount(Units.pound);
            gallons = getAmount(Units.gallon);


            //Units defaultUnit = 

            //chemicalAmount = DisplayContainers.Sum(con => con.con.Amount);
            uniqueBuildings = DisplayContainers.Select(con => con.loc.BuildingName).Distinct().Count();
            numContainers = DisplayContainers.Count();
        }

        /// <summary>
        /// Gets a queryable of valid display containers
        /// </summary>
        /// <param name="ignoreCase">Whether our search filters are case sensitive</param>
        /// <returns>A Queryable containing every valid container matching search criteria</returns>
        public IQueryable<DisplayContainer> validSearchItems(bool ignoreCase)
        {
            var checkCase = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            //  EF.Functions.Like( is necesary to get this server side (can't use .contains()
            var containers = from con in _context.X_Container
                             join conChem in _context.ContainerChemicals on con.ContainerID equals conChem.ContainerID
                             // join acc in _context.Account on con.SupervisorId equals acc.AccountId
                             join loc in _context.Location on con.RoomId equals loc.RoomId
                             where (string.IsNullOrEmpty(searchCAS)        || EF.Functions.Like(conChem.ChemicalCAS, "%" + searchCAS + "%")) &&
                                   (string.IsNullOrEmpty(searchString)     || EF.Functions.Like(con.Product_Name, "%" + searchString + "%")) &&
                                   (string.IsNullOrEmpty(searchBuilding)   || loc.BuildingName.ToString().Equals(searchBuilding)) &&
                                   (string.IsNullOrEmpty(searchSize)       || con.Amount != Int32.Parse(searchSize)) &&
                                   // (string.IsNullOrEmpty(searchEmail)      || EF.Functions.Like(acc.Email, "%" + searchEmail + "%")) && // 
                                   // (string.IsNullOrEmpty(searchUnits)      || con.Unit.Equals((Units)Int32.Parse(searchUnits))) && // do we even need to keep this? 
                                   // (string.IsNullOrEmpty(searchDepartment) || acc.Department.ToString().Equals(searchDepartment)) // should use location
                             select new DisplayContainer(con, chem, loc, acc);

            return containers;
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
                Models.Container con = (from c in _context.Container
                                 where c.ContainerId == Int32.Parse(Request.Form["ContainerID"])
                                 select c).Single();
                con.CasNumber = Request.Form["Cas Number"];
                /*
                // Orginal, but supervisorID is now taken from Location
                con.SupervisorId = (from s in _context.User
                                    where s.Name.Equals(Request.Form["Supervisor"], StringComparison.OrdinalIgnoreCase)
                                    select s.AccountId).FirstOrDefault();
                */
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
            Models.Container newCon = new Models.Container();
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
                /*
                // Orginal. but supversiorName would have to be found by connecting the supervisorID from location connecting back to User.Name
                var supervisor = _context.Account.FirstOrDefault(x => x.Name == supervisorName);
                */
                /*
                // Orginal. But supervisorID is grabbed from location now
                //newCon.SupervisorId = supervisor.AccountId;
                */
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

        public async Task GetDisplayContainer()
        {
            DisplayContainers = await validSearchItems(false).ToListAsync();
        }

        public DisplayContainer GetListItem(int index)
        { 
            return DisplayContainers[index];
        }

        /* Removed because it was stupid and it doesn't seem like it was used for anything anyway - Yasmin
        public JsonResult OnGetListItem(int containerListIndex)
        {
            var returnVal = GetListItem(containerListIndex);
            List<string> returnList = new()
            {
                returnVal.chem.ChemicalName,
                returnVal.chem.CasNumber,
                returnVal.supervisor.Name,
                returnVal.con.Amount.ToString(),
                returnVal.con.ContainerId.ToString(),
                returnVal.loc.BuildingName.ToString(),
                returnVal.loc.RoomNumber,
                returnVal.chem.ChemicalHazards.ToString()
            };
            return new JsonResult(returnList);
        }
        */

        /// <summary>
        /// Runs on every search and returns a list of containers that fit the given search criteria
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            DisplayContainers = await validSearchItems(false).ToListAsync();

            updateQuickReference();

            //if the revNums button was pressed
            if (revNums)
            {
                //Inverse the state of the number search
                revNumsPrev = !revNumsPrev;
                //Keep the sort method the same
                sortMethod = prevSort;
            }
            else
            {
                //Check if the same button was pushed again
                if (prevSort == sortMethod)
                {
                    //Reverses the sort method
                    sortMethod++;
                }
                prevSort = sortMethod;
            }

            //Switch case to determine what to sort by initially
            //Has to be sorted on reload with current setup
            IOrderedEnumerable<DisplayContainer> temp = sortMethod switch
            {
                1 => DisplayContainers.OrderByDescending(c => c.con.Product_Name),
                2 => DisplayContainers.OrderBy(c => c.con.CasNumber),
                3 => DisplayContainers.OrderByDescending(c => c.con.CasNumber),
                4 => DisplayContainers.OrderBy(c => c.loc.BuildingName).ThenBy(c => c.con.Product_Name),
                5 => DisplayContainers.OrderByDescending(c => c.loc.BuildingName).ThenBy(c => c.con.Product_Name),

                _ => DisplayContainers.OrderBy(c => c.con.Product_Name)
            };

            //Always sort by size of container
            DisplayContainers = revNumsPrev ? temp.ThenBy(c => c.con.Unit).ThenBy(c => c.con.Amount).ToList()
                                            : temp.ThenByDescending(c => c.con.Unit).ThenByDescending(c => c.con.Amount).ToList();
        }

        public PartialViewResult OnGetEditModal(long conid)
        {
            var container = (from con in _context.Container
                             join conChem in _context.ContainerChemicals on con.id equals conChem.ContainerID
                             where con.ContainerId == conid
                             select new ViewModels.ContainerViewModel
                             {
                                 ContainerName = con.ProductName,
                                 ContainerId = con.ContainerId,
                                 Unit = con.Unit,
                                 Amount = con.Amount,
                                 Retired = con.Retired,
                                 CasNumber = con.CasNumber,
                                 RoomId = con.RoomId,
                                 SupervisorId = con.SupervisorId
                             }).FirstOrDefault();
            return Partial("_EditModal", container);
        }

        public PartialViewResult OnGetAddModal()
        {
            return Partial("_AddModal", new ContainerViewModel());
        }
        public IActionResult OnGetAutoComplete(string term)
        {
            var names = _context.User.Where(a => a.Name.Contains(term)).Select(a => a.Name).Take(3).ToList();
            return new JsonResult(names);
        }
    }
}