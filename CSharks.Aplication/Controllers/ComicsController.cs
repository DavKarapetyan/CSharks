using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class ComicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
