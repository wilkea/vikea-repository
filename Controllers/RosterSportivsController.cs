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
    public class RosterSportivsController : Controller
    {
        private readonly dpcContext _context;

        public RosterSportivsController(dpcContext context)
        {
            _context = context;
        }

        // GET: RosterSportivs
        public async Task<IActionResult> Index()
        {
            var dpcContext = _context.RosterSportivs.Include(r => r.Roster).Include(r => r.Sportiv);
            return View(await dpcContext.ToListAsync());
        }

        // GET: RosterSportivs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rosterSportiv = await _context.RosterSportivs
                .Include(r => r.Roster)
                .Include(r => r.Sportiv)
                .FirstOrDefaultAsync(m => m.RosterSportivId == id);
            if (rosterSportiv == null)
            {
                return NotFound();
            }

            return View(rosterSportiv);
        }

        // GET: RosterSportivs/Create
        public IActionResult Create()
        {
            ViewData["RosterId"] = new SelectList(_context.Rosters
                .Include(r => r.Organizatie)
                .Select(r => new
                {
                    RosterId = r.RosterId,
                    DisplayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                }), "RosterId", "DisplayText");
            ViewData["SportivId"] = new SelectList(_context.Sportivs, "SportivId", "Nume");
            return View();
        }

        // POST: RosterSportivs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RosterSportivId,RosterId,SportivId")] RosterSportiv rosterSportiv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rosterSportiv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RosterId"] = new SelectList(_context.Rosters
                .Include(r => r.Organizatie)
                .Select(r => new
                {
                    RosterId = r.RosterId,
                    DisplayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                }), "RosterId", "DisplayText", rosterSportiv.RosterId);
            ViewData["SportivId"] = new SelectList(_context.Sportivs, "SportivId", "Nume", rosterSportiv.SportivId);
            return View(rosterSportiv);
        }

        // GET: RosterSportivs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rosterSportiv = await _context.RosterSportivs.FindAsync(id);
            if (rosterSportiv == null)
            {
                return NotFound();
            }
            ViewData["RosterId"] = new SelectList(_context.Rosters
                .Include(r => r.Organizatie)
                .Select(r => new
                {
                    RosterId = r.RosterId,
                    DisplayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                }), "RosterId", "DisplayText", rosterSportiv.RosterId);
            ViewData["SportivId"] = new SelectList(_context.Sportivs, "SportivId", "Nume", rosterSportiv.SportivId);
            return View(rosterSportiv);
        }

        // POST: RosterSportivs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RosterSportivId,RosterId,SportivId")] RosterSportiv rosterSportiv)
        {
            if (id != rosterSportiv.RosterSportivId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rosterSportiv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterSportivExists(rosterSportiv.RosterSportivId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RosterId"] = new SelectList(_context.Rosters
                .Include(r => r.Organizatie)
                .Select(r => new
                {
                    RosterId = r.RosterId,
                    DisplayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                }), "RosterId", "DisplayText", rosterSportiv.RosterId);
            ViewData["SportivId"] = new SelectList(_context.Sportivs, "SportivId", "Nume", rosterSportiv.SportivId);
            return View(rosterSportiv);
        }

        // GET: RosterSportivs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rosterSportiv = await _context.RosterSportivs
                .Include(r => r.Roster)
                .Include(r => r.Sportiv)
                .FirstOrDefaultAsync(m => m.RosterSportivId == id);
            if (rosterSportiv == null)
            {
                return NotFound();
            }

            return View(rosterSportiv);
        }

        // POST: RosterSportivs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rosterSportiv = await _context.RosterSportivs.FindAsync(id);
            if (rosterSportiv != null)
            {
                _context.RosterSportivs.Remove(rosterSportiv);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RosterSportivExists(int id)
        {
            return _context.RosterSportivs.Any(e => e.RosterSportivId == id);
        }
    }
}
