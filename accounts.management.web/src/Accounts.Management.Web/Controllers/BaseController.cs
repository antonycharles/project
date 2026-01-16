using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Accounts.Management.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly AccountsManagementSettings _configuration;

        public BaseController(IOptions<AccountsManagementSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["FileApiUrlPublic"] = _configuration.FileApiUrlPublic;
            base.OnActionExecuting(context);
        }
    }
}