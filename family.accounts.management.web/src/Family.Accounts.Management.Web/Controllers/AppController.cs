using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Repositories;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Family.Accounts.Management.Web.Controllers
{
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

        public async Task<IActionResult> IndexAsync(PaginatedRequest? request)
        {
            try
            {
                var apps = await _appRepository.GetAsync(request);
                return View(apps);

            }
            catch(Exception ex){
                var teste = ex.Message;
                return View(new PaginatedResponse<AppResponse>(){
                    Request = new PaginatedRequest(),
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AppRequest request)
        {
            try
            {
                await _appRepository.CreateAsync(request);
                return RedirectToAction("Index");
            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                var teste = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id){
            try
            {
                var app = await _appRepository.GetByIdAsync(id);
                return View(new AppRequest{
                    Name = app.Name,
                    Status = app.Status,
                });
            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                var teste = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> EditAsync(Guid id, AppRequest request)
        {
            try
            {
                await _appRepository.UpdateAsync(id, request);
                return RedirectToAction("Index");
            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                var teste = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _appRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return RedirectToAction("Index");
            }
            catch(Exception ex){
                var teste = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}