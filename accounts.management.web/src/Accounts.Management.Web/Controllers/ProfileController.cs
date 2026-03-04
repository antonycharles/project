using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Accounts.Management.Web.Helpers;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Management.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IAppRepository _appRepository;
        private readonly IPermissionRepository _permissionRepository;

        public ProfileController(
            IProfileRepository profileRepository,
            IAppRepository appRepository,
            IPermissionRepository permissionRepository)
        {
            _profileRepository = profileRepository;
            _appRepository = appRepository;
            _permissionRepository = permissionRepository;
        }

        [AuthorizeRole(RoleConstants.ProfileRole.List)]
        public async Task<IActionResult> IndexAsync(ProfilePaginatedRequest? request)
        {
            try
            {
                request ??= new ProfilePaginatedRequest();
                request.PageSize = 1000;

                if (request.AppId == null || request.AppId == Guid.Empty)
                {
                    HttpContext.AddMessageError("App is required to list profiles.");
                    return RedirectToAction("Index", "App");
                }

                var app = await _appRepository.GetByIdAsync(request.AppId.Value);
                var profiles = await _profileRepository.GetAsync(request);

                return View(new AppProfileViewModel
                {
                    App = app,
                    Profiles = profiles
                });
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch
            {
                return View(new AppProfileViewModel
                {
                    App = new AppResponse(),
                    Profiles = new PaginatedResponse<ProfileResponse>
                    {
                        Request = new PaginatedRequest(),
                    }
                });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ProfileRole.List)]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            try
            {
                var profile = await _profileRepository.GetByIdAsync(id);
                return RedirectToAction("Permissions", new { id = profile.Id });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ProfileRole.Update)]
        public async Task<IActionResult> PermissionsAsync(Guid id)
        {
            try
            {
                var profile = await _profileRepository.GetByIdAsync(id);
                var availablePermissions = await _permissionRepository.GetAsync(new PermissionPaginatedRequest
                {
                    AppId = profile.AppId,
                    PageSize = 1000
                });

                return View(new ProfilePermissionManagementViewModel
                {
                    Profile = profile,
                    AppId = profile.AppId,
                    ProfileId = profile.Id,
                    PermissionIds = profile.Permissions?.Select(x => x.Id).ToArray() ?? Array.Empty<Guid>(),
                    AvailablePermissions = availablePermissions
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ProfileRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PermissionsAsync(ProfilePermissionManagementViewModel model)
        {
            try
            {
                await _profileRepository.UpdatePermissionsAsync(model.ProfileId, model.PermissionIds ?? Array.Empty<Guid>());
                HttpContext.AddMessageSuccess("Profile permissions updated successfully!");
                return RedirectToAction("Permissions", new { id = model.ProfileId });
            }
            catch (ExternalApiException ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Permissions", new { id = model.ProfileId });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Permissions", new { id = model.ProfileId });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ProfileRole.Create)]
        public async Task<IActionResult> CreateAsync(Guid appId)
        {
            var app = await _appRepository.GetByIdAsync(appId);
            ViewBag.App = app;

            return View(new ProfileRequest
            {
                AppId = appId
            });
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ProfileRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ProfileRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.App = await _appRepository.GetByIdAsync(request.AppId ?? Guid.Empty);
                    return View(request);
                }

                await _profileRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("Profile created success!");
                return RedirectToAction("Index", new { appId = request.AppId });
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId ?? Guid.Empty);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId ?? Guid.Empty);
                return View(request);
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ProfileRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            try
            {
                var profile = await _profileRepository.GetByIdAsync(id);
                ViewBag.App = await _appRepository.GetByIdAsync(profile.AppId);

                return View(new ProfileRequest
                {
                    Name = profile.Name,
                    Slug = profile.Slug,
                    Type = profile.Type,
                    AppId = profile.AppId,
                    IsDefault = profile.IsDefault,
                    Status = profile.Status,
                    PermissionIds = profile.Permissions?.Select(x => x.Id).ToArray()
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ProfileRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, ProfileRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.App = await _appRepository.GetByIdAsync(request.AppId ?? Guid.Empty);
                    return View(request);
                }

                await _profileRepository.UpdateAsync(id, request);
                if (request.PermissionIds != null)
                {
                    await _profileRepository.UpdatePermissionsAsync(id, request.PermissionIds);
                }

                HttpContext.AddMessageSuccess("Profile update success!");
                return RedirectToAction("Index", new { appId = request.AppId });
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId ?? Guid.Empty);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId ?? Guid.Empty);
                return View(request);
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ProfileRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id)
        {
            try
            {
                var profile = await _profileRepository.GetByIdAsync(id);
                return View(profile);
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ProfileRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id, Guid appId)
        {
            try
            {
                await _profileRepository.DeleteAsync(id);
                HttpContext.AddMessageSuccess("Profile delete success!");
                return RedirectToAction("Index", new { appId });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("DeleteConfirmAsync", new { id });
            }
        }
    }
}
