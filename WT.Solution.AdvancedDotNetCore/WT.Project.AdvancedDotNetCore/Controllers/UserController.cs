using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WT.Project.AdvancedDotNetCore.Services;

namespace WT.Project.AdvancedDotNetCore.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUsers _users;

        public UserController(IUsers users)
        {
            _users = users;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(int userId)
        {
            var user = await _users.WhereIdIs(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}