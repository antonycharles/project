using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Exceptions;
using Family.Accounts.Login.Infra.Repositories;
using Family.Accounts.Login.Infra.Requests;
using Family.Accounts.Login.Infra.Responses;
using Family.Accounts.Login.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Family.Accounts.Login.Web.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserAuthorizationRepository _userAuthorizationRepository;

        public LoginController(ILogger<LoginController> logger, IUserAuthorizationRepository userAuthorizationRepository)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(string? AppSlug = "")
        {
            return View(new UserAuthenticationRequest
            {
                AppSlug = AppSlug
            });
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(UserAuthenticationRequest request){
            try
            {
                if (!ModelState.IsValid)
                    return View(request);
                
                var result = await _userAuthorizationRepository.AuthenticateAsync(request);
                var userInfo = await _userAuthorizationRepository.GetUserInfoByIdAsync(result.AuthId.ToString());

                await AddCookieAuthentication(result, userInfo);

                return Redirect(result.CallbackUrl ?? Url.Action("Index", "Home"));
            }
            catch(ExternalApiException ex)
            {
                base.AddModelError(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(request);
        }

        private async Task AddCookieAuthentication(AuthenticationResponse auth, UserResponse userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userInfo.Id.ToString()),
                new Claim(ClaimTypes.Name, userInfo.Name),
                new Claim(ClaimTypes.Email, userInfo.Email)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Family.Accounts.Login.Web", principal);
        }
    }
}