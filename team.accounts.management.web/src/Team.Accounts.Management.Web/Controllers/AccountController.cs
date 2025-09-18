using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Repositories;
using Team.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Team.Accounts.Management.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Team.Accounts.Management.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountsManagementSettings _settings;
        private readonly ILoginRepository _loginRepository;

        public AccountController(
            ILogger<AccountController> logger, 
            IOptions<AccountsManagementSettings> settings,
            ILoginRepository loginRepository)
        {
            _logger = logger;
            _settings = settings.Value;
            _loginRepository = loginRepository;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Redirect(_settings.TeamAcountsLoginRedirectUrl + "/login?appSlug=" + _settings.TeamAccountsManagementSlug);
        }

        [HttpGet("Callback")]
        public async Task<IActionResult> Callback(string code)
        {
            try
            {
                var authenticationResponse = await _loginRepository.GetByCodeAsync(code);
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(authenticationResponse.Token);
                var claims = jwtSecurityToken.Claims.ToList();

                var identity = new ClaimsIdentity(claims, "CookieManagement");
                var principal = new ClaimsPrincipal(identity);

                var expire = authenticationResponse.ExpiresIn - DateTime.Now;
                var minutes = expire != null ? (int)expire.Value.TotalMinutes : 180;
                
                await HttpContext.SignInAsync("CookieManagement", principal, new AuthenticationProperties{
                    ExpiresUtc = DateTimeOffset.Now.AddMinutes(minutes),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during authentication callback");
            }

            return Redirect(Url.Action("Index", "Home"));
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieManagement");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}