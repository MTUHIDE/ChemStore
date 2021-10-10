﻿using System;
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
            if (!string.IsNullOrEmpty(searchUnits) && !con.con.Unit.Equals((Units) Int32.Parse(searchUnits)))
                return false;
            if (!string.IsNullOrEmpty(searchDepartment) && !con.supervisor.Department.ToString().Equals(searchDepartment))
                return false;
            if (!string.IsNullOrEmpty(searchRetired) && bool.Parse(searchRetired) != con.con.Retired)
                return false;

            return true;
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

            // Checks each search term against the current list of containers
            /*
            if (!string.IsNullOrEmpty(searchCAS))
                containers = containers.Where(c => c.CasNumber.Contains(searchCAS, checkCase));
            if (!string.IsNullOrEmpty(searchString))
                containers = containers.Where(c => c.CasNumberNavigation.ChemicalName.Contains(searchString, checkCase));
            //if (!string.IsNullOrEmpty(searchBuilding))
            //    containers = containers.Where(c => c.Room.BuildingName.Equals((Buildings)Int32.Parse(searchBuilding)));
            //if (!string.IsNullOrEmpty(searchSize))
            //    containers = containers.Where(c => c.Amount.Equals(Int32.Parse(searchSize)));
            //if (!string.IsNullOrEmpty(searchEmail))
            //    containers = containers.Where(c => c.Supervisor.Email.Contains(searchEmail, checkCase));
            //if (!string.IsNullOrEmpty(searchUnits))
            //    containers = containers.Where(c => c.Unit.Equals((Units)Int32.Parse(searchUnits)));
            //if (!string.IsNullOrEmpty(searchDepartment))
            //    containers = containers.Where(c => c.Supervisor.Department.Equals((Departments)Int32.Parse(searchDepartment)));
            //if (!string.IsNullOrEmpty(searchRetired))
            //   containers = containers.Where(c => c.Retired == bool.Parse(searchRetired));

            Containers = await containers.ToListAsync();

            //Containers = await _context.Container.ToListAsync();*/
        }
    }
}