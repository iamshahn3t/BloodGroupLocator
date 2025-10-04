using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodGroupLocator.Web.Data;
using BloodGroupLocator.Web.Models;

namespace BloodGroupLocator.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly BloodGroupLocatorContext _context;

        public PersonController(BloodGroupLocatorContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,BloodGroup,Phone,Email,Latitude,Longitude")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.CreatedAt = DateTime.Now;
                person.UpdatedAt = DateTime.Now;
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,BloodGroup,Phone,Email,Latitude,Longitude,CreatedAt")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    person.UpdatedAt = DateTime.Now;
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Person/Locate
        public IActionResult Locate()
        {
            return View();
        }

        // POST: Person/Locate
        [HttpPost]
        public async Task<IActionResult> Locate(string bloodGroup, double? latitude, double? longitude, double radius = 50)
        {
            var persons = await _context.Persons
                .Where(p => p.BloodGroup == bloodGroup)
                .ToListAsync();

            if (latitude.HasValue && longitude.HasValue)
            {
                // Filter by distance if coordinates are provided
                persons = persons
                    .Where(p => p.Latitude.HasValue && p.Longitude.HasValue)
                    .Where(p => CalculateDistance(latitude.Value, longitude.Value, p.Latitude.Value, p.Longitude.Value) <= radius)
                    .ToList();
            }

            ViewBag.BloodGroup = bloodGroup;
            ViewBag.Latitude = latitude;
            ViewBag.Longitude = longitude;
            ViewBag.Radius = radius;

            return View("LocateResults", persons);
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Earth's radius in kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
