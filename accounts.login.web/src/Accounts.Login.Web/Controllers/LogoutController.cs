using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Login.Web.Controllers
{
    [Route("[controller]")]
    public class LogoutController : BaseController
    {
        private readonly ILogger<LogoutController> _logger;

        public LogoutController(ILogger<LogoutController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Login");

            await HttpContext.SignOutAsync("CookieAuth");

            return RedirectToAction("Index", "Login");
        }
    }
}