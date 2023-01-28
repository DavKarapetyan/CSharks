using CSharks.BLL.Services.Interfaces;
using javax.jws;
using jdk.nashorn.@internal.runtime;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Text.Encodings.Web;

namespace CSharks.Aplication.Controllers
{
    public class QuizTypeController : Controller
    {
        private readonly IQuizTypeService _quizTypeService;
        public QuizTypeController(IQuizTypeService quizTypeService)
        {
            _quizTypeService = quizTypeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [WebMethod]
        public void GetAllQuizTypes()
        {
            var data = _quizTypeService.GetQuizTypes();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Response.WriteAsJsonAsync(js.Serialize(data));
        }
    }
}
