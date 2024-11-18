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
    public class RosterEvenimentsController : Controller
    {
        private readonly dpcContext _context;

        public RosterEvenimentsController(dpcContext context)
        {
            _context = context;
        }

        // GET: RosterEveniments
        public async Task<IActionResult> Index()
        {
            var dpcContext = _context.RosterEveniments.Include(r => r.Eveniment).Include(r => r.Roster);
            return View(await dpcContext.ToListAsync());
        }

        // GET: RosterEveniments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rosterEveniment = await _context.RosterEveniments
                .Include(r => r.Eveniment)
                .Include(r => r.Roster)
                .FirstOrDefaultAsync(m => m.RosterEvenimentId == id);
            if (rosterEveniment == null)
            {
                return NotFound();
            }

            return View(rosterEveniment);
        }

        // GET: RosterEveniments/Create
        public IActionResult Create()
        {
            ViewData["EvenimentId"] = new SelectList(_context.Eveniments, "EvenimentId", "Denumire");
            ViewData["OrganizatieId"] = new SelectList(_context.Organizaties, "OrganizatieId", "Denumire");
            ViewData["RosterId"] = new SelectList(_context.Rosters
                .Include(r => r.Organizatie)
                .Select(r => new
                {
                    RosterId = r.RosterId,
                    DisplayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                }), "RosterId", "DisplayText");
            return View();
        }

        [HttpGet]
        public JsonResult GetRosters(int OrganizatieId)
        {
            var rosters = _context.Rosters
                .Where(r => r.OrganizatieId == OrganizatieId)
                .Select(r => new
                {
                    rosterId = r.RosterId,
                    displayText = $"{r.Disciplina} - {r.Organizatie.Denumire}"
                })
                .ToList();
            
            return Json(rosters);
        }

        // POST: RosterEveniments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RosterEvenimentId,EvenimentId,RosterId")] RosterEveniment rosterEveniment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rosterEveniment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvenimentId"] = new SelectList(_context.Eveniments, "EvenimentId", "Denumire", rosterEveniment.EvenimentId);
            ViewData["RosterId"] = new SelectList(_context.Rosters, "RosterId", "Disciplina", rosterEveniment.RosterId);
            return View(rosterEveniment);
        }

        // GET: RosterEveniments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rosterEveniment = await _context.RosterEveniments.FindAsync(id);
            if (rosterEveniment == null)
            {
                return NotFound();
            }
            ViewData["EvenimentId"] = new SelectList(_context.Eveniments, "EvenimentId", "Denumire", rosterEveniment.EvenimentId);
            ViewData["RosterId"] = new SelectList(_context.Rosters, "RosterId", "Disciplina", rosterEveniment.RosterId);
            return View(rosterEveniment);
        }

        // POST: RosterEveniments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RosterEvenimentId,EvenimentId,RosterId")] RosterEveniment rosterEveniment)
        {
            if (id != rosterEveniment.RosterEvenimentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rosterEveniment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterEvenimentExists(rosterEveniment.RosterEvenimentId))
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
            ViewData["EvenimentId"] = new SelectList(_context.Eveniments, "EvenimentId", "Denumire", rosterEveniment.EvenimentId);
            ViewData["RosterId"] = new SelectList(_context.Rosters, "RosterId", "Disciplina", rosterEveniment.RosterId);
            return View(rosterEveniment);
        }

        // GET: RosterEveniments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rosterEveniment = await _context.RosterEveniments
                .Include(r => r.Eveniment)
                .Include(r => r.Roster)
                .FirstOrDefaultAsync(m => m.RosterEvenimentId == id);
            if (rosterEveniment == null)
            {
                return NotFound();
            }

            return View(rosterEveniment);
        }

        // POST: RosterEveniments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rosterEveniment = await _context.RosterEveniments.FindAsync(id);
            if (rosterEveniment != null)
            {
                _context.RosterEveniments.Remove(rosterEveniment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RosterEvenimentExists(int id)
        {
            return _context.RosterEveniments.Any(e => e.RosterEvenimentId == id);
        }
    }
}
