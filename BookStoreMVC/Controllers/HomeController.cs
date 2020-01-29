using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BookStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Error")]
        [HttpGet]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}