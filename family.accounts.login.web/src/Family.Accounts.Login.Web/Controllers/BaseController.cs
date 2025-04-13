using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Family.Accounts.Login.Web.Controllers
{
    public abstract class BaseController : Controller
    {

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