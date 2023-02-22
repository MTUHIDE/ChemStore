using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChemStoreWebApp.Pages.Admin.Chemicals
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly chemstoreContext _context;

        public CreateModel(chemstoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public Chemical Chemical { get; set; }

        [BindProperty]
        public bool Corrosion { get; set; }

        [BindProperty]
        public bool Environment { get; set; }

        [BindProperty]
        public bool ExclamationMark { get; set; }

        [BindProperty]
        public bool ExplodingBomb { get; set; }

        [BindProperty]
        public bool FlameOverCircle { get; set; }

        [BindProperty]
        public bool Flame { get; set; }

        [BindProperty]
        public bool GasCylinder { get; set; }

        [BindProperty]
        public bool HealthHazard { get; set; }

        [BindProperty]
        public bool Skull { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chemical.Add(Chemical);
            await _context.SaveChangesAsync();

            // hazard mapping
            if (Corrosion)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "Corrosion"
                });
            }
            if (Environment)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "Environment"
                });
            }
            if (ExclamationMark)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "ExclamationMark"
                });
            }
            if (ExplodingBomb)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "ExplodingBomb"
                });
            }
            if (FlameOverCircle)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "FlameOverCircle"
                });
            }
            if (Flame)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "Flame"
                });
            }
            if (GasCylinder)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "GasCylinder"
                });
            }
            if (HealthHazard)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "HealthHazard"
                });
            }
            if (Skull)
            {
                _context.ChemicalHazards.Add(new ChemicalHazards
                {
                    CasNumber = Chemical.CasNumber,
                    HazardId = "Skull"
                });
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
