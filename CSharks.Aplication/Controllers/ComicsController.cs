using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class ComicsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
