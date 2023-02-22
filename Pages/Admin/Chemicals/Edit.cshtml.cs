using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChemStoreWebApp.Pages.Admin.Chemicals
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public EditModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
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

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chemical = await _context.Chemical.FirstOrDefaultAsync(m => m.CasNumber == id);

            if (Chemical == null)
            {
                return NotFound();
            }

            var hazards = _context.ChemicalHazards.Where(a => a.CasNumber == Chemical.CasNumber).ToList();

            var exi = hazards.Where(a => a.HazardId == "Corrosion").FirstOrDefault();
            if (exi != null)
                Corrosion = true;

            exi = hazards.Where(a => a.HazardId == "Environment").FirstOrDefault();
            if (exi != null)
                Environment = true;

            exi = hazards.Where(a => a.HazardId == "ExclamationMark").FirstOrDefault();
            if (exi != null)
                ExclamationMark = true;

            exi = hazards.Where(a => a.HazardId == "ExplodingBomb").FirstOrDefault();
            if (exi != null)
                ExplodingBomb = true;

            exi = hazards.Where(a => a.HazardId == "FlameOverCircle").FirstOrDefault();
            if (exi != null)
                FlameOverCircle = true;

            exi = hazards.Where(a => a.HazardId == "Flame").FirstOrDefault();
            if (exi != null)
                Flame = true;

            exi = hazards.Where(a => a.HazardId == "HealthHazard").FirstOrDefault();
            if (exi != null)
                HealthHazard = true;

            exi = hazards.Where(a => a.HazardId == "GasCylinder").FirstOrDefault();
            if (exi != null)
                GasCylinder = true;

            exi = hazards.Where(a => a.HazardId == "Skull").FirstOrDefault();
            if (exi != null)
                Skull = true;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Chemical).State = EntityState.Modified;

            // hazard mapping
            var hazards = _context.ChemicalHazards.Where(a => a.CasNumber == Chemical.CasNumber).ToList();

            if (Corrosion)
            {
                if (!hazards.Any(a => a.HazardId == "Corrosion"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "Corrosion"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "Corrosion").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (Environment)
            {
                if (!hazards.Any(a => a.HazardId == "Environment"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "Environment"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "Environment").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (ExclamationMark)
            {
                if (!hazards.Any(a => a.HazardId == "ExclamationMark"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "ExclamationMark"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "ExclamationMark").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (ExplodingBomb)
            {
                if (!hazards.Any(a => a.HazardId == "ExplodingBomb"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "ExplodingBomb"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "ExplodingBomb").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (FlameOverCircle)
            {
                if (!hazards.Any(a => a.HazardId == "FlameOverCircle"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "FlameOverCircle"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "FlameOverCircle").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (Flame)
            {
                if (!hazards.Any(a => a.HazardId == "Flame"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "Flame"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "Flame").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (GasCylinder)
            {
                if (!hazards.Any(a => a.HazardId == "GasCylinder"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "GasCylinder"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "GasCylinder").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (HealthHazard)
            {
                if (!hazards.Any(a => a.HazardId == "HealthHazard"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "HealthHazard"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "HealthHazard").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }
            if (Skull)
            {
                if (!hazards.Any(a => a.HazardId == "Skull"))
                {
                    _context.ChemicalHazards.Add(new ChemicalHazards
                    {
                        CasNumber = Chemical.CasNumber,
                        HazardId = "Skull"
                    });
                }
            }
            else
            {
                var rec = hazards.Where(a => a.HazardId == "Skull").FirstOrDefault();
                if (rec != null)
                    _context.ChemicalHazards.Remove(rec);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChemicalExists(Chemical.CasNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChemicalExists(string id)
        {
            return _context.Chemical.Any(e => e.CasNumber == id);
        }
    }
}
