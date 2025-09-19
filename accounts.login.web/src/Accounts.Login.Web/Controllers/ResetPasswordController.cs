using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Login.Web.Controllers
{
    [Route("[controller]")]
    public class ResetPasswordController : Controller
    {
        private readonly ILogger<ResetPasswordController> _logger;

        public ResetPasswordController(ILogger<ResetPasswordController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}