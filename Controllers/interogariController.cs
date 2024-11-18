using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dpcleague_2.Data;
using dpcleague_2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace dpcleague_2.Controllers
{
    [Route("interogari")]
    public class interogariController : Controller
    {
        private readonly dpcContext _context;

        public interogariController(dpcContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("upcoming-events")]
        public async Task<IActionResult> UpcomingEvents()
        {
            var events = await _context.Eveniments
                .Where(e => e.DataInceput > DateTime.Now)
                .OrderBy(e => e.DataInceput)
                .ToListAsync();
            return View(events);
        }

        [HttpGet("top-organizations")]
        public async Task<IActionResult> TopOrganizations()
        {
            var organizations = await _context.Organizaties
                .Select(o => new
                {
                    o.Denumire,
                    o.Originea,
                    SportiviCount = o.Sportivi.Count
                })
                .OrderByDescending(o => o.SportiviCount)
                .ToListAsync();
            return View(organizations);
        }

        [HttpGet("players-teams")]
        public async Task<IActionResult> PlayersAndTeams()
        {
            var players = await _context.Sportivs
                .Include(s => s.Organizatie)
                .Select(s => new
                {
                    NumeComplet = s.Nume + " " + s.Prenume,
                    s.Porecla,
                    Echipa = s.Organizatie.Denumire
                })
                .ToListAsync();
            return View(players);
        }

        [HttpGet("popular-events")]
        public async Task<IActionResult> PopularEvents()
        {
            var events = await _context.Eveniments
                .Select(e => new
                {
                    e.Denumire,
                    e.DataInceput,
                    BiletCount = e.Bilets.Count
                })
                .OrderByDescending(e => e.BiletCount)
                .ToListAsync();
            return View(events);
        }

        [HttpGet("organization-rosters")]
        public async Task<IActionResult> OrganizationRosters()
        {
            var rosters = await _context.Organizaties
                .Include(o => o.Rostere)
                .Select(o => new
                {
                    o.Denumire,
                    Rostere = o.Rostere.Select(r => new {
                        r.Disciplina,
                        r.DataFormare
                    }).ToList()
                })
                .ToListAsync();
            return View(rosters);
        }

        [HttpGet("events-by-discipline")]
        public async Task<IActionResult> EventsByDiscipline()
        {
            var events = await _context.Eveniments
                .GroupBy(e => e.Disciplina)
                .Select(g => new
                {
                    Disciplina = g.Key,
                    Evenimente = g.ToList()
                })
                .ToListAsync();
            return View(events);
        }

        [HttpGet("player-age-stats")]
        public async Task<IActionResult> PlayerAgeStats()
        {
            var stats = await _context.Rosters
                
                .Include(r => r.RosterSportivi)
                    .ThenInclude(rs => rs.Sportiv)
                .Where(r => r.RosterSportivi.Any())
                .Select(r => new
                {
                    OrganizatieDenumire = r.Organizatie.Denumire,
                    r.Disciplina,
                    VarstaMedie = r.RosterSportivi.Average(rs => DateTime.Now.Year - rs.Sportiv.DataNasterii.Year)
                })
                .OrderByDescending(r => r.VarstaMedie)
                .ToListAsync();

            return View(stats);
        }

        [HttpGet("ticket-stats")]
        public async Task<IActionResult> TicketStats()
        {
            var stats = await _context.Eveniments
                .Select(e => new
                {
                    e.Denumire,
                    TotalBilete = e.Bilets.Count,
                })
                .ToListAsync();
            return View(stats);
        }

        [HttpGet("organizations-by-origin")]
        public async Task<IActionResult> OrganizationsByOrigin()
        {
            var organizations = await _context.Organizaties
                .GroupBy(o => o.Originea)
                .Select(g => new
                {
                    Origine = g.Key,
                    Organizatii = g.Select(o => new
                    {
                        o.Denumire,
                        o.DataCrearii,
                        NumarSportivi = o.Sportivi.Count
                    }).ToList()
                })
                .ToListAsync();
            return View(organizations);
        }

        [HttpGet("events-full-details")]
        public async Task<IActionResult> EventFullDetails()
        {
            var events = await _context.Eveniments
                .Select(e => new
                {
                    e.Denumire,
                    e.Disciplina,
                    e.DataInceput,
                    e.Locatia,
                    NumarBilete = e.Bilets.Count,
                    NumarEchipe = e.RosterEvenimente.Count
                })
                .ToListAsync();
            return View(events);
        }

        [HttpGet("roster-organization-stats")]
        public async Task<IActionResult> RosterOrganizationStats()
        {
            var stats = await _context.Rosters
                .Include(r => r.Organizatie)
                .Include(r => r.RosterSportivi)
                    .ThenInclude(rs => rs.Sportiv)
                .Select(r => new
                {
                    RosterDisciplina = r.Disciplina,
                    OrganizatieDenumire = r.Organizatie.Denumire,
                    r.Disciplina,
                    r.DataFormare,
                    NumarSportivi = r.RosterSportivi.Count,
                    Sportivi = r.RosterSportivi.Select(rs => new
                    {
                        NumeComplet = rs.Sportiv.Nume + " " + rs.Sportiv.Prenume,
                        rs.Sportiv.Porecla,
                        Varsta = DateTime.Now.Year - rs.Sportiv.DataNasterii.Year
                    }).ToList()
                })
                .OrderBy(r => r.OrganizatieDenumire)
                .ThenBy(r => r.DataFormare)
                .ToListAsync();
            return View(stats);
        }
    }
}