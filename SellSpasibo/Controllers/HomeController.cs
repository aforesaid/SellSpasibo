using Microsoft.AspNetCore.Mvc;
using SellSpasibo.Models.ViewModels;

namespace SellSpasibo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetRub(OrderRubViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            var i = 1;
            return View();
        }
    }
}
