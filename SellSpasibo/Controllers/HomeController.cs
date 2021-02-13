using Microsoft.AspNetCore.Mvc;

namespace SellSpasibo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
