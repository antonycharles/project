using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Mappers;
using Accounts.Management.Infrastructure.Repositories;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Accounts.Management.Web.Helpers;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Management.Web.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;
        private readonly IAppRepository _appRepository;
        private readonly IFileRepository _fileRepository;

        public AppController(
            ILogger<AppController> logger,
            IAppRepository appRepository,
            IFileRepository fileRepository)
        {
            _logger = logger;
            _appRepository = appRepository;
            _fileRepository = fileRepository;
        }

        [AuthorizeRole(RoleConstants.AppRole.List)]
        public async Task<IActionResult> IndexAsync(PaginatedRequest? request)
        {
            try
            {
                var apps = await _appRepository.GetAsync(request);
                return View(apps);

            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                return View(new PaginatedResponse<AppResponse>(){
                    Request = new PaginatedRequest(),
                });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.AppRole.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AppViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                await AddFavicon(request);

                await _appRepository.CreateAsync(new AppRequest
                {
                    Code = request.Code.Value,
                    Name = request.Name,
                    Slug = request.Slug,
                    CallbackUrl = request.CallbackUrl,
                    FaviconUrl = request.FaviconUrl,
                    Status = request.Status.Value
                });
                HttpContext.AddMessageSuccess("App created success!");
                return RedirectToAction("Index");
            }
            catch (ExternalApiException ex){
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
            catch(Exception ex){
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        private async Task AddFavicon(AppViewModel request)
        {
            if (request.FaviconFile != null)
            {
                var uploadResult = await _fileRepository.UploadAsync(request.FaviconFile);
                request.FaviconUrl = uploadResult.Url;
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id){
            try
            {
                var app = await _appRepository.GetByIdAsync(id);
                return View(new AppViewModel{
                    Code = app.Code,
                    Name = app.Name,
                    Slug = app.Slug,
                    CallbackUrl = app.CallbackUrl,
                    FaviconUrl = app.FaviconUrl,
                    Status = app.Status
                });
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, AppViewModel request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View();

                await AddFavicon(request);
                
                await _appRepository.UpdateAsync(id, new AppRequest
                {
                    Code = request.Code.Value,
                    Name = request.Name,
                    Slug = request.Slug,
                    CallbackUrl = request.CallbackUrl,
                    FaviconUrl = request.FaviconUrl,
                    Status = request.Status.Value
                });

                HttpContext.AddMessageSuccess("App update success!");
                return RedirectToAction("Index");
            }
            catch(ExternalApiException ex){
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
            catch(Exception ex){
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }


        [HttpGet]
        [AuthorizeRole(RoleConstants.AppRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id){
            try
            {
                var app = await _appRepository.GetByIdAsync(id);
                return View(app);
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _appRepository.DeleteAsync(id);
                HttpContext.AddMessageSuccess("App delete success!");
                return RedirectToAction("Index");
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("DeleteConfirmAsync", new { id = id});
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}