using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WT.Project.AdvancedDotNetCore.Services;

namespace WT.Project.AdvancedDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployees _employees;

        public HomeController(IEmployees employees)
        {
            _employees = employees;
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _employees.All());
        }
    }
}
