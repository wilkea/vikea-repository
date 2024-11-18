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
    public class RostersController : Controller
    {
        private readonly dpcContext _context;

        public RostersController(dpcContext context)
        {
            _context = context;
        }

        // GET: Rosters
        public async Task<IActionResult> Index()
        {
            var dpcContext = _context.Rosters.Include(r => r.Organizatie);
            return View(await dpcContext.ToListAsync());
        }

        // GET: Rosters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .Include(r => r.Organizatie)
                .FirstOrDefaultAsync(m => m.RosterId == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // GET: Rosters/Create
        public IActionResult Create()
        {
            ViewData["OrganizatieId"] = new SelectList(_context.Organizaties, "OrganizatieId", "Denumire");
            return View();
        }

        // POST: Rosters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RosterId,OrganizatieId,Disciplina,DataFormare")] Roster roster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizatieId"] = new SelectList(_context.Organizaties, "OrganizatieId", "Denumire", roster.OrganizatieId);
            return View(roster);
        }

        // GET: Rosters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters.FindAsync(id);
            if (roster == null)
            {
                return NotFound();
            }
            ViewData["OrganizatieId"] = new SelectList(_context.Organizaties, "OrganizatieId", "Denumire", roster.OrganizatieId);
            return View(roster);
        }

        // POST: Rosters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RosterId,OrganizatieId,Disciplina,DataFormare")] Roster roster)
        {
            if (id != roster.RosterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterExists(roster.RosterId))
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
            ViewData["OrganizatieId"] = new SelectList(_context.Organizaties, "OrganizatieId", "Denumire", roster.OrganizatieId);
            return View(roster);
        }

        // GET: Rosters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .Include(r => r.Organizatie)
                .FirstOrDefaultAsync(m => m.RosterId == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // POST: Rosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roster = await _context.Rosters.FindAsync(id);
            if (roster != null)
            {
                _context.Rosters.Remove(roster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RosterExists(int id)
        {
            return _context.Rosters.Any(e => e.RosterId == id);
        }
    }
}
