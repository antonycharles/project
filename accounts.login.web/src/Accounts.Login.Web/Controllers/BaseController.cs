using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Exceptions;
using Accounts.Login.Infra.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly AccountsLoginSettings _configuration;

        public BaseController(IOptions<AccountsLoginSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["FileApiUrlPublic"] = _configuration.FileApiUrlPublic;
            base.OnActionExecuting(context);
        }

        public void AddModelError(ExternalApiException ex)
        {
            if(ex.Errors != null && ex.Errors.Any())
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}