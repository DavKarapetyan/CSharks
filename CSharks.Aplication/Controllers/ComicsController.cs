using CSharks.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class ComicsController : BaseController
    {
        private readonly IComicsService _comicsService;
        public ComicsController(IComicsService comicsService)
        {
            _comicsService = comicsService;
        }
        public IActionResult Index()
        {
            var data = _comicsService.GetAllComices(CurrentCulture);
            return View(data);
        }
    }
}
