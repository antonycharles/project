using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Exceptions;
using Accounts.Login.Infra.Repositories;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Infra.Settings;
using Accounts.Login.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Web.Controllers
{
    [Route("[controller]")]
    public class SignUpController : BaseController
    {
        private readonly ILogger<SignUpController> _logger;
        private readonly IUserRepository _userRepository;

        public SignUpController(
            ILogger<SignUpController> logger, 
            IUserRepository userRepository,
            IOptions<AccountsLoginSettings> configuration) : base(configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string? AppSlug = "")
        {
            return View(new UserRequest
            {
                AppSlug = AppSlug
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(request);

                request.CreateCompanyDefault = true;
                var user = await _userRepository.CreateAsync(request);

                HttpContext.SetSuccessResponse("User created successfully.");
                
                return Redirect(Url.Action("Index", "Login", new { AppSlug = request.AppSlug }));
            }
            catch (ExternalApiException ex)
            {
                base.AddModelError(ex);
            }
            catch (Exception ex)
            {
                return RedirectToError(
                    _logger,
                    ex,
                    "Não foi possível concluir seu cadastro neste momento.",
                    returnAction: "Index",
                    returnController: "SignUp",
                    routeValues: new { request.AppSlug },
                    returnLabel: "Voltar para cadastro");
            }

            return View(request);
        }
    }
}
