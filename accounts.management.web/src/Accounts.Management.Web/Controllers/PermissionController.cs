using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Accounts.Management.Infrastructure.Settings;
using Accounts.Management.Web.Helpers;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Web.Controllers
{
    [Authorize]
    public class PermissionController : BaseController
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAppRepository _appRepository;

        public PermissionController(
            IPermissionRepository permissionRepository,
            IOptions<AccountsManagementSettings> settings,
            IAppRepository appRepository) : base(settings)
        {
            _permissionRepository = permissionRepository;
            _appRepository = appRepository;
        }

        [AuthorizeRole(RoleConstants.PermissionRole.List)]
        public async Task<IActionResult> IndexAsync(PermissionPaginatedRequest? request)
        {
            try
            {
                request ??= new PermissionPaginatedRequest();
                request.PageSize = 1000;

                if (request.AppId == Guid.Empty)
                {
                    HttpContext.AddMessageError("App is required to list permissions.");
                    return RedirectToAction("Index", "App");
                }

                var app = await _appRepository.GetByIdAsync(request.AppId);
                var permissions = await _permissionRepository.GetAsync(request);
                return View(new AppPermissionViewModel
                {
                    App = app,
                    Permissions = permissions
                });

            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch
            {
                return View(new AppPermissionViewModel
                {
                    App = new AppResponse(),
                    Permissions = new PaginatedResponse<PermissionResponse>
                    {
                        Request = new PaginatedRequest(),
                    }
                });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.PermissionRole.Create)]
        public async Task<IActionResult> CreateAsync(Guid appId)
        {
            var app = await _appRepository.GetByIdAsync(appId);
            ViewBag.App = app;
            return View(new PermissionRequest
            {
                AppId = appId
            });
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.PermissionRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(PermissionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                    return View(request);
                }

                await _permissionRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("Permission created success!");
                return RedirectToAction("Index", new { appId = request.AppId });
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                return View(request);
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.PermissionRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            try
            {
                var permission = await _permissionRepository.GetByIdAsync(id);
                ViewBag.App = await _appRepository.GetByIdAsync(permission.AppId);
                return View(new PermissionRequest
                {
                    Name = permission.Name,
                    Role = permission.Role,
                    AppId = permission.AppId,
                    PermissionFatherId = permission.PermissonFatherId,
                    Status = permission.Status
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.PermissionRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, PermissionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                    return View(request);
                }

                await _permissionRepository.UpdateAsync(id, request);
                HttpContext.AddMessageSuccess("Permission update success!");
                return RedirectToAction("Index", new { appId = request.AppId });
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                return View(request);
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.PermissionRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id)
        {
            try
            {
                var permission = await _permissionRepository.GetByIdAsync(id);
                return View(permission);
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.PermissionRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id, Guid appId)
        {
            try
            {
                await _permissionRepository.DeleteAsync(id);
                HttpContext.AddMessageSuccess("Permission delete success!");
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
