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
            return Ok();
        }
    }
}
