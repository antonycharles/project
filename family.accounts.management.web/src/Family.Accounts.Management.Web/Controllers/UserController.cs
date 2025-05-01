using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Repositories;
using Family.Accounts.Management.Infrastructure.Mappers;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Family.Accounts.Management.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Family.Accounts.Management.Web.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(
            ILogger<UserController> logger,
            IUserRepository appRepository)
        {
            _logger = logger;
            _userRepository = appRepository;
        }

        [AuthorizeRole(RoleConstants.UserRole.List)]
        public async Task<IActionResult> IndexAsync(PaginatedRequest? request)
        {
            try
            {
                var apps = await _userRepository.GetAsync(request);
                return View(apps);

            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                return View(new PaginatedResponse<UserResponse>(){
                    Request = new PaginatedRequest(),
                });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.UserRole.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(UserRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View();

                await _userRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("User created success!");
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
        [AuthorizeRole(RoleConstants.UserRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id){
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return View(user.ToUserRequest());
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, UserRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View();

                await _userRepository.UpdateAsync(id, request);

                HttpContext.AddMessageSuccess("User update success!");
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
        [AuthorizeRole(RoleConstants.UserRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id){
            try
            {
                var app = await _userRepository.GetByIdAsync(id);
                return View(app);
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                HttpContext.AddMessageSuccess("User delete success!");
                return RedirectToAction("Index");
            }
            catch(Exception ex){
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("DeleteConfirmAsync", new { id = id});
            }
        }

    }
}