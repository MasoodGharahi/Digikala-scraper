using Digikala_scraper.Models;
using Digikala_scraper.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Digikala_scraper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            Product retrievedProduct = await Scraper.GetAttributesAsync("https://www.digikala.com/product/dkp-1452117/%D9%86%D8%B1%D8%AF%D8%A8%D8%A7%D9%86-%DA%86%D9%87%D8%A7%D8%B1-%D9%BE%D9%84%D9%87-%D9%85%D8%AF%D9%84-%DA%A9%D8%A7%D8%B1%D8%A7%D8%B3%D8%A7%D9%86-%DA%A9%D8%AF-26010002/");
            return View();
        }
    }
}