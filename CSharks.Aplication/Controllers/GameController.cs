using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
