using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Exceptions;
using Team.Accounts.Login.Infra.Repositories;
using Team.Accounts.Login.Infra.Repositories.Interfaces;
using Team.Accounts.Login.Infra.Requests;
using Team.Accounts.Login.Infra.Responses;
using Team.Accounts.Login.Web.Extensions;
using Team.Accounts.Login.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Team.Accounts.Login.Web.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IDistributedCache _cache;
        private readonly IUserAuthorizationRepository _userAuthorizationRepository;
        private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

        public LoginController(
            ILogger<LoginController> logger, 
            IUserAuthorizationRepository userAuthorizationRepository,
            IDistributedCache cache)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(string? AppSlug = "")
        {
            try
            {
                //if (User.Identity.IsAuthenticated && User.GetRefreshToken() != null)
                //{
                //    var result = await _userAuthorizationRepository.RefreshTokenAsync(User.GetRefreshToken(), AppSlug);
                //    return await GenerateCode(result);
                //}

                return View(new UserAuthenticationRequest
                {
                    AppSlug = AppSlug
                });
            }
            catch (ExternalApiException ex)
            {
                base.AddModelError(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

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
                var userInfo = await _userAuthorizationRepository.GetUserInfoByTokenAsync(result.Token);

                await AddCookieAuthentication(result, userInfo);

                return await GenerateCode(result);
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

        [HttpGet("Token")]
        public async Task<IActionResult> TokenAsync(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                    return BadRequest("Invalid authentication response.");

                var json = await _cache.GetStringAsync(code);
                if (string.IsNullOrEmpty(json))
                    return BadRequest("Invalid authentication response.");

                var auth = JsonSerializer.Deserialize<AuthenticationResponse>(json);
                if (auth == null)
                    return BadRequest("Invalid authentication response.");

                try
                {
                    await _cache.RemoveAsync(code);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the request.");
                }

                return Ok(auth);
            }
            catch (ExternalApiException ex)
            {
                base.AddModelError(ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return BadRequest(ex.Message);
            }
        }

        private async Task<IActionResult> GenerateCode(AuthenticationResponse result)
        {
            var code = Guid.NewGuid().ToString();

            await _cache.SetStringAsync(
                code,
                JsonSerializer.Serialize(result),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
            );

            if (result.CallbackUrl != null && result.CallbackUrl != "")
                return Redirect($"{result.CallbackUrl}?code={code}");
            else
                return Redirect(Url.Action("Index", "Home"));
        }
        
        private async Task AddCookieAuthentication(AuthenticationResponse auth, UserResponse userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userInfo.Id.ToString()),
                new Claim(ClaimTypes.Name, userInfo.Name),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim("RefreshToken", auth.RefreshToken)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);
        }
    }
}