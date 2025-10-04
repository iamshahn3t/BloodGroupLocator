using System.Diagnostics;
using BloodGroupLocator.Web.Models;
using BloodGroupLocator.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodGroupLocator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BloodGroupLocatorContext _context;

        public HomeController(ILogger<HomeController> logger, BloodGroupLocatorContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Locate
        public IActionResult Locate()
        {
            return View();
        }

        // POST: Home/Locate
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
