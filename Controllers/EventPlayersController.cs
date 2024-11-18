using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dpcleague_2.Data;
using dpcleague_2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dpcleague_2.Controllers
{
    public class EventPlayersController : Controller
    {
        private readonly dpcContext _context;

        public EventPlayersController(dpcContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? evenimentId)
        {
            ViewData["Evenimente"] = new SelectList(_context.Eveniments, "EvenimentId", "Denumire", evenimentId);

            if (!evenimentId.HasValue)
            {
                return View(new List<Sportiv>());
            }

            var players = await _context.RosterEveniments
                .Where(re => re.EvenimentId == evenimentId)
                .Include(re => re.Roster)
                    .ThenInclude(r => r.RosterSportivi)
                        .ThenInclude(rs => rs.Sportiv)
                            .ThenInclude(s => s.Organizatie)
                .SelectMany(re => re.Roster.RosterSportivi.Select(rs => rs.Sportiv))
                .Distinct()
                .ToListAsync();

            return View(players);
        }
    }
} 