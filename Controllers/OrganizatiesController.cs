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
    public class OrganizatiesController : Controller
    {
        private readonly dpcContext _context;

        public OrganizatiesController(dpcContext context)
        {
            _context = context;
        }

        // GET: Organizaties
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizaties.ToListAsync());
        }

        // GET: Organizaties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Organizatie = await _context.Organizaties
                .FirstOrDefaultAsync(m => m.OrganizatieId == id);
            if (Organizatie == null)
            {
                return NotFound();
            }

            return View(Organizatie);
        }

        // GET: Organizaties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizaties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizatieId,Denumire,DataCrearii,Originea")] Organizatie Organizatie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Organizatie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Organizatie);
        }

        // GET: Organizaties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Organizatie = await _context.Organizaties.FindAsync(id);
            if (Organizatie == null)
            {
                return NotFound();
            }
            return View(Organizatie);
        }

        // POST: Organizaties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizatieId,Denumire,DataCrearii,Originea")] Organizatie Organizatie)
        {
            if (id != Organizatie.OrganizatieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Organizatie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizatieExists(Organizatie.OrganizatieId))
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
            return View(Organizatie);
        }

        // GET: Organizaties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Organizatie = await _context.Organizaties
                .FirstOrDefaultAsync(m => m.OrganizatieId == id);
            if (Organizatie == null)
            {
                return NotFound();
            }

            return View(Organizatie);
        }

        // POST: Organizaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Organizatie = await _context.Organizaties.FindAsync(id);
            if (Organizatie != null)
            {
                _context.Organizaties.Remove(Organizatie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizatieExists(int id)
        {
            return _context.Organizaties.Any(e => e.OrganizatieId == id);
        }
    }
}
