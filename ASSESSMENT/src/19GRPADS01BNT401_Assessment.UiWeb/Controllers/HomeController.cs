using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _19GRPADS01BNT401_Assessment.UiWeb.Models;

namespace _19GRPADS01BNT401_Assessment.UiWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
