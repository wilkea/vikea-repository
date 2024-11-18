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
    public class EvenimentsController : Controller
    {
        private readonly dpcContext _context;

        public EvenimentsController(dpcContext context)
        {
            _context = context;
        }

        // GET: Eveniments
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DenumireSortParm"] = String.IsNullOrEmpty(sortOrder) ? "denumire_desc" : "";
            ViewData["DisciplinaSortParm"] = sortOrder == "disciplina" ? "disciplina_desc" : "disciplina";
            ViewData["DataSortParm"] = sortOrder == "data" ? "data_desc" : "data";
            ViewData["LocatiaSortParm"] = sortOrder == "locatia" ? "locatia_desc" : "locatia";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var evenimente = from e in _context.Eveniments
                            select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                evenimente = evenimente.Where(e => e.Denumire.Contains(searchString)
                                             || e.Disciplina.Contains(searchString)
                                             || e.Locatia.Contains(searchString));
            }

            evenimente = sortOrder switch
            {
                "denumire_desc" => evenimente.OrderByDescending(e => e.Denumire),
                "disciplina" => evenimente.OrderBy(e => e.Disciplina),
                "disciplina_desc" => evenimente.OrderByDescending(e => e.Disciplina),
                "data" => evenimente.OrderBy(e => e.DataInceput),
                "data_desc" => evenimente.OrderByDescending(e => e.DataInceput),
                "locatia" => evenimente.OrderBy(e => e.Locatia),
                "locatia_desc" => evenimente.OrderByDescending(e => e.Locatia),
                _ => evenimente.OrderBy(e => e.Denumire),
            };

            int pageSize = 5;
            return View(await PaginatedList<Eveniment>.CreateAsync(evenimente.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Eveniments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eveniment = await _context.Eveniments
                .FirstOrDefaultAsync(m => m.EvenimentId == id);
            if (eveniment == null)
            {
                return NotFound();
            }

            return View(eveniment);
        }

        // GET: Eveniments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eveniments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvenimentId,Denumire,Disciplina,DataInceput,Locatia")] Eveniment eveniment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if denumire is unique
                    if (await _context.Eveniments.AnyAsync(e => e.Denumire == eveniment.Denumire))
                    {
                        ModelState.AddModelError("Denumire", "Acest eveniment există deja!");
                        return View(eveniment);
                    }

                    eveniment.Bilets = new List<Bilet>();
                    eveniment.RosterEvenimente = new List<RosterEveniment>();
                    
                    _context.Add(eveniment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving: " + ex.Message);
            }

            return View(eveniment);
        }

        // GET: Eveniments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eveniment = await _context.Eveniments.FindAsync(id);
            if (eveniment == null)
            {
                return NotFound();
            }
            return View(eveniment);
        }

        // POST: Eveniments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvenimentId,Denumire,Disciplina,DataInceput,Locatia")] Eveniment eveniment)
        {
            if (id != eveniment.EvenimentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if denumire is unique (excluding current eveniment)
                if (await _context.Eveniments.AnyAsync(e => e.Denumire == eveniment.Denumire && e.EvenimentId != eveniment.EvenimentId))
                {
                    ModelState.AddModelError("Denumire", "Acest eveniment există deja!");
                    return View(eveniment);
                }

                try
                {
                    _context.Update(eveniment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvenimentExists(eveniment.EvenimentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(eveniment);
        }

        // GET: Eveniments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eveniment = await _context.Eveniments
                .FirstOrDefaultAsync(m => m.EvenimentId == id);
            if (eveniment == null)
            {
                return NotFound();
            }

            return View(eveniment);
        }

        // POST: Eveniments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eveniment = await _context.Eveniments.FindAsync(id);
            if (eveniment != null)
            {
                _context.Eveniments.Remove(eveniment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvenimentExists(int id)
        {
            return _context.Eveniments.Any(e => e.EvenimentId == id);
        }
    }
}
