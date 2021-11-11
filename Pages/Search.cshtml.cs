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

public interface ICategoryService
{
    IEnumerable<Building> GetCategories();
}

public class CategoryService : ICategoryService
{
    public IEnumerable<Building> GetCategories()
    {
        var enumValues = Enum.GetValues(typeof(Buildings)).Cast<Buildings>().ToList();
        List<Building> returnList = new List<Building>();
        foreach (var item in enumValues)
        {
            returnList.Add(new Building { buildingIndex = (int)item, buildingName = item.ToString() });
        }
        return returnList;
    }
}

namespace ChemStoreWebApp.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;
        private ICategoryService categoryService;

        public SearchModel(ChemStoreWebApp.Models.chemstoreContext context, ICategoryService categoryService)
        {
            _context = context;
            this.categoryService = categoryService;
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
        public List<int> chemicalsToDelete { get; set; }
        public List<SelectListItem> RoomNumbers { get; set; }
        [BindProperty(SupportsGet = true)]
        public int buildingIndex { get; set; }
        public int SubCategoryId { get; set; }
        public SelectList Categories { get; set; }

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
        public List<int> deleteFromDatabase(List<int> containerIds)
        {
            foreach (int id in containerIds)
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
            if (!string.IsNullOrEmpty(searchCAS) && !con.chem.CasNumber.Contains(searchString, checkCase))
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

        public async Task<IActionResult> OnPostCreate()
        {
            Container newCon = new Container();
            newCon.CasNumber = Request.Form["CAS Number"];
            newCon.Unit = 0;
            newCon.Retired = false;
            var roomName = Request.Form["Room"];
            var buildingName = Request.Form["Building"];
            var buildingInt = (int)(Buildings)Enum.Parse(typeof(Buildings), buildingName);
            var location = _context.Location.Single(x => x.BuildingName == buildingInt && x.RoomNumber == roomName);
            newCon.RoomId = location.RoomId;
            var supervisorName = Request.Form["Supervisor"];
            var supervisor = _context.Account.SingleOrDefault(x => x.Name == supervisorName);
            newCon.SupervisorId = supervisor.AccountId;
            newCon.Amount = Convert.ToInt32(Request.Form["Amount"]);
            addToDatabase(newCon);
            return RedirectToPage();
        }

        public IEnumerable<Location> GetSubCategories(int? categoryId)
        {
            var subCategories = _context.Location.Select(x =>
                new Location { BuildingName = x.BuildingName, RoomId = x.RoomId, RoomNumber = x.RoomNumber });
            return subCategories.Where(s => s.BuildingName == categoryId);
        }
        public JsonResult OnGetSubCategories()
        {
            return new JsonResult(GetSubCategories(buildingIndex));
        }

        public async Task<IActionResult> OnPostGetRoomNumbers()
        {

            var buildingName = Request.Form["Building"];
            var buildingInt = (int)(Buildings)Enum.Parse(typeof(Buildings), buildingName);
            var chosenRooms = _context.Location.Select(x => x).Where(x => x.BuildingName == buildingInt);
            RoomNumbers = chosenRooms.Select(location =>
                                    new SelectListItem
                                    {
                                        Value = location.RoomNumber,
                                        Text = location.RoomNumber
                                    }).ToList();
            await OnGet();
            return Page();
        }

        /// <summary>
        /// Runs on every search and returns a list of containers that fit the given search criteria
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
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

            Categories = new SelectList(categoryService.GetCategories(), nameof(Building.buildingIndex), nameof(Building.buildingName));


        }
    }
}