using Microsoft.AspNetCore.Mvc;
using MultiCashApp.Data;
using MultiCashApp.Models;
using System.Diagnostics;

namespace MultiCashApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _databaseContext;

        public HomeController(ILogger<HomeController> logger, DatabaseContext databaseContext)
        {

            //var date= DateTime.Now;

            //var Date = new DateTime(2022, 1, 1);
            var Date = new DateTime(2025, 1, 1);

            var x = System.Globalization.ISOWeek.GetWeekOfYear(Date);

            _logger = logger;
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            var test = _databaseContext.Users.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                var viewName = statusCode.ToString();
                return View("~/Views/Error/" + viewName + ".cshtml");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}