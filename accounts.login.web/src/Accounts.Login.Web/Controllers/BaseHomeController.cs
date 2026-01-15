using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Exceptions;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Settings;
using Accounts.Login.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Web.Controllers
{
    public abstract class BaseHomeController : Controller
    {
        private readonly AccountsLoginSettings _configuration;
        private readonly ICompanyRepository _companyRepository;

        public BaseHomeController(IOptions<AccountsLoginSettings> configuration, ICompanyRepository companyRepository)
        {
            _configuration = configuration.Value;
            _companyRepository = companyRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["FileApiUrlPublic"] = _configuration.FileApiUrlPublic;
            base.OnActionExecuting(context);
            ViewData["Companies"] = _companyRepository.GetCompaniesByUserIdAsync(User.GetId()).GetAwaiter().GetResult().ToList();
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