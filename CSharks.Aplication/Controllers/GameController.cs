using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class GameController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
