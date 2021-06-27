using Microsoft.AspNetCore.Mvc;
using SellSpasibo.API.Models.ViewModels;

namespace SellSpasibo.API.Controllers
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
