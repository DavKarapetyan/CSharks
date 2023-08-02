using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharks.Aplication.Components
{
    public class NewsViewComponent : ViewComponent
    {
        private readonly INewsService _newsService;
        protected CultureType CurrentCulture
        {
            get
            {
                var cultureInfo = Request.HttpContext.Features.Get<IRequestCultureFeature>();

                var culture = cultureInfo.RequestCulture.Culture.Name;
                return (CultureType)Enum.Parse(typeof(CultureType), culture);
            }
        }
        public NewsViewComponent(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IViewComponentResult Invoke()
        {
            var data = _newsService.GetAllNews(CurrentCulture, null, null);
            return View(data);
        }
    }
}
