using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Exceptions;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Web.Extensions;
using Accounts.Login.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Accounts.Login.Infra.Responses;

namespace Accounts.Login.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IUserPhotoRepository _userPhotoRepository;

        public UserController(
            ILogger<UserController> logger,
            IUserRepository userRepository = null,
            IFileRepository fileRepository = null,
            IUserPhotoRepository userPhotoRepository = null)
        {
            _logger = logger;
            _userRepository = userRepository;
            _fileRepository = fileRepository;
            _userPhotoRepository = userPhotoRepository;
        }

        [HttpGet]
        public IActionResult UpdateAsync()
        {
            return View(new UserUpdateViewModel
            {
                Name = User.GetName(),
                Email = User.GetEmail()
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(UserUpdateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                await _userRepository.UpdateAsync(User.GetId(), new UserUpdateRequest
                {
                    Name = model.Name,
                    Email = model.Email
                });

                FileDocumentResponse document = null;
                if (model.Image != null && model.Image.Length > 0)
                {
                    using var stream = model.Image.OpenReadStream();
                    document = await _fileRepository.UploadAsync(model.Image);
                    await _userPhotoRepository.AddUserPhotoAsync(new UserPhotoRequest
                    {
                        UserId = User.GetId(),
                        DocumentId = document.Id,
                        DocumentUrl = document.Url
                    });
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, User.GetId().ToString()),
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim("RefreshToken", User.GetRefreshToken() ?? ""),
                    new Claim("image", document?.Url ?? "")
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
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
            
            return View(model);
        }
    }
}