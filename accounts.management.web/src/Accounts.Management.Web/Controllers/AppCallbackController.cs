using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Web.Helpers;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Web.Controllers
{
    [Authorize]
    public class AppCallbackController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly IAppCallbackRepository _appCallbackRepository;

        public AppCallbackController(
            IAppRepository appRepository,
            IAppCallbackRepository appCallbackRepository)
        {
            _appRepository = appRepository;
            _appCallbackRepository = appCallbackRepository;
        }

        [AuthorizeRole(RoleConstants.AppRole.List)]
        public async Task<IActionResult> IndexAsync(Guid appId)
        {
            try
            {
                var app = await _appRepository.GetByIdAsync(appId);
                var callbacks = await _appCallbackRepository.GetAllByAppIdAsync(appId);
                return View(new AppCallbackPageViewModel
                {
                    App = app,
                    Callbacks = callbacks
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        public async Task<IActionResult> CreateAsync(Guid appId)
        {
            ViewBag.App = await _appRepository.GetByIdAsync(appId);
            return View(new AppCallbackRequest
            {
                AppId = appId
            });
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AppCallbackRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                    return View(request);
                }

                await _appCallbackRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("Callback created success!");
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
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            try
            {
                var callback = await _appCallbackRepository.GetByIdAsync(id);
                ViewBag.App = await _appRepository.GetByIdAsync(callback.AppId);
                return View(new AppCallbackRequest
                {
                    AppId = callback.AppId,
                    Environment = callback.Environment,
                    IsDefault = callback.IsDefault,
                    Url = callback.Url
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, AppCallbackRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.App = await _appRepository.GetByIdAsync(request.AppId);
                    return View(request);
                }

                await _appCallbackRepository.UpdateAsync(id, request);
                HttpContext.AddMessageSuccess("Callback update success!");
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
        [AuthorizeRole(RoleConstants.AppRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id)
        {
            try
            {
                var callback = await _appCallbackRepository.GetByIdAsync(id);
                return View(callback);
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id, Guid appId)
        {
            try
            {
                await _appCallbackRepository.DeleteAsync(id);
                HttpContext.AddMessageSuccess("Callback delete success!");
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
