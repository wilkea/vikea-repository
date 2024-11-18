using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dpcleague_2.Data;
using dpcleague_2.Models;

namespace dpcleague_2.Controllers
{
    public class SportivsController : Controller
    {
        private readonly dpcContext _context;

        public SportivsController(dpcContext context)
        {
            _context = context;
        }

        // GET: Sportivs
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int? organizatieId)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NumeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nume_desc" : "";
            ViewData["PrenumeSortParm"] = sortOrder == "prenume" ? "prenume_desc" : "prenume";
            ViewData["PoreclaSortParm"] = sortOrder == "porecla" ? "porecla_desc" : "porecla";
            ViewData["OrganizatieSortParm"] = sortOrder == "organizatie" ? "organizatie_desc" : "organizatie";

            ViewBag.OrganizatieId = new SelectList(_context.Organizaties, "OrganizatieId", "Denumire", organizatieId);

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentOrganizatie"] = organizatieId;

            var sportivi = from s in _context.Sportivs
                           .Include(s => s.Organizatie)
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchLower = searchString.ToLower();
                sportivi = sportivi.Where(s => s.Nume.ToLower().Contains(searchLower)
                                             || s.Prenume.ToLower().Contains(searchLower)
                                             || s.Porecla.ToLower().Contains(searchLower));
            }

            if (organizatieId.HasValue)
            {
                sportivi = sportivi.Where(s => s.OrganizatieId == organizatieId);
            }

            sportivi = sortOrder switch
            {
                "nume_desc" => sportivi.OrderByDescending(s => s.Nume),
                "prenume" => sportivi.OrderBy(s => s.Prenume),
                "prenume_desc" => sportivi.OrderByDescending(s => s.Prenume),
                "porecla" => sportivi.OrderBy(s => s.Porecla),
                "porecla_desc" => sportivi.OrderByDescending(s => s.Porecla),
                "organizatie" => sportivi.OrderBy(s => s.Organizatie.Denumire),
                "organizatie_desc" => sportivi.OrderByDescending(s => s.Organizatie.Denumire),
                _ => sportivi.OrderBy(s => s.Nume),
            };

            int pageSize = 10;
            return View(await PaginatedList<Sportiv>.CreateAsync(sportivi.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Sportivs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportiv = await _context.Sportivs
                .Include(s => s.Organizatie)
                .FirstOrDefaultAsync(m => m.SportivId == id);
            if (sportiv == null)
            {
                return NotFound();
            }

            return View(sportiv);
        }

        // GET: Sportivs/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Sportivs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportivId,Nume,Prenume,DataNasterii,OrganizatieId,Porecla,RosterId")] Sportiv sportiv, int? RosterId)
        {
            if (ModelState.IsValid)
            {
                // Check if porecla is unique
                if (await _context.Sportivs.AnyAsync(s => s.Porecla == sportiv.Porecla))
                {
                    ModelState.AddModelError("Porecla", "Această poreclă există deja!");
                    PopulateDropDownLists(sportiv.OrganizatieId, RosterId);
                    return View(sportiv);
                }

                _context.Add(sportiv);
                await _context.SaveChangesAsync();

                // If RosterId is provided, create RosterSportiv entry
                if (RosterId.HasValue)
                {
                    var rosterSportiv = new RosterSportiv
                    {
                        RosterId = RosterId.Value,
                        SportivId = sportiv.SportivId
                    };
                    _context.RosterSportivs.Add(rosterSportiv);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists(sportiv.OrganizatieId, RosterId);
            return View(sportiv);
        }

        // GET: Sportivs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportiv = await _context.Sportivs.FindAsync(id);
            if (sportiv == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(sportiv.OrganizatieId);
            return View(sportiv);
        }

        // POST: Sportivs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportivId,Nume,Prenume,DataNasterii,OrganizatieId,Porecla")] Sportiv sportiv)
        {
            if (id != sportiv.SportivId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if porecla is unique (excluding current sportiv)
                if (await _context.Sportivs.AnyAsync(s => s.Porecla == sportiv.Porecla && s.SportivId != sportiv.SportivId))
                {
                    ModelState.AddModelError("Porecla", "Această poreclă există deja!");
                    PopulateDropDownLists(sportiv.OrganizatieId);
                    return View(sportiv);
                }

                try
                {
                    _context.Update(sportiv);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportivExists(sportiv.SportivId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            PopulateDropDownLists(sportiv.OrganizatieId);
            return View(sportiv);
        }

        // GET: Sportivs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportiv = await _context.Sportivs
                .Include(s => s.Organizatie)
                .FirstOrDefaultAsync(m => m.SportivId == id);
            if (sportiv == null)
            {
                return NotFound();
            }

            return View(sportiv);
        }

        // POST: Sportivs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportiv = await _context.Sportivs
                .Include(s => s.RosterSportivi)
                .FirstOrDefaultAsync(s => s.SportivId == id);

            if (sportiv != null)
            {
                // Remove all RosterSportiv entries for this player
                if (sportiv.RosterSportivi != null)
                {
                    _context.RosterSportivs.RemoveRange(sportiv.RosterSportivi);
                }
                
                // Remove the player
                _context.Sportivs.Remove(sportiv);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SportivExists(int id)
        {
            return _context.Sportivs.Any(e => e.SportivId == id);
        }

        private void PopulateDropDownLists(int? selectedOrganizatieId = null, int? selectedRosterId = null)
        {
            var organizatii = _context.Organizaties.ToList();
            ViewBag.OrganizatieId = new SelectList(organizatii, "OrganizatieId", "Denumire", selectedOrganizatieId);

            // Get all rosters with their organizations, without filtering
            var rosters = _context.Rosters
                .Include(r => r.Organizatie)
                .Select(r => new
                {
                    RosterId = r.RosterId,
                    DisplayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                })
                .ToList();

            ViewBag.RosterId = new SelectList(rosters, "RosterId", "DisplayText", selectedRosterId);
        }
    }
}
    