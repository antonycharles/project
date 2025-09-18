using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Mappers;
using Family.Accounts.Management.Infrastructure.Repositories;
using Family.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Family.Accounts.Management.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Family.Accounts.Management.Web.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;
        private readonly IAppRepository _appRepository;

        public AppController(
            ILogger<AppController> logger,
            IAppRepository appRepository)
        {
            _logger = logger;
            _appRepository = appRepository;
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
        public async Task<IActionResult> CreateAsync(AppRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View();

                await _appRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("App created success!");
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
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id){
            try
            {
                var app = await _appRepository.GetByIdAsync(id);
                return View(app.ToAppRequest());
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.AppRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, AppRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View();

                await _appRepository.UpdateAsync(id, request);

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