using Microsoft.AspNetCore.Mvc;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.WEB.Models;
using System.Diagnostics;

namespace RedFalcon.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactServices _contact;
        public HomeController(ILogger<HomeController> logger, IContactServices contact)
        {
            _logger = logger;
            _contact = contact;
        }

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