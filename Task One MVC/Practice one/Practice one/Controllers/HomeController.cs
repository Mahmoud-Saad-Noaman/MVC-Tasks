using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Practice_one.Models;

namespace Practice_one.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Home/Show
        public ContentResult Show()
        {
            ContentResult result = new ContentResult();
            result.Content = "Hello every one !";
            return result;
        }

        //Home/ShowView
        public ViewResult ShowView()
        {
            ViewResult result = new ViewResult();
            result.ViewName = "View1";
            return result;
        }


        public IActionResult ShowEven(int id)
        {
            if (id % 2 == 0)
            {
                return View("Even");
            }
            else
            {
                return Content("Odd");
            }
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
