using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using WT.Project.AdvancedDotNetCore.Services;

namespace WT.Project.AdvancedDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployees _employees;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(IEmployees employees, IHtmlLocalizer<HomeController> localizer)
        {
            _employees = employees;
            _localizer = localizer;
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["HelloWorld"] = _localizer["HelloWorld"];
            return View(await _employees.All());
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);
        }
    }
}
