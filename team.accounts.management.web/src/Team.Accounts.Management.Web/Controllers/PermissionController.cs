using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Exceptions;
using Team.Accounts.Management.Infrastructure.Repositories;
using Team.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Team.Accounts.Management.Infrastructure.Requests;
using Team.Accounts.Management.Infrastructure.Responses;
using Team.Accounts.Management.Web.Helpers;
using Team.Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Team.Accounts.Management.Web.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAppRepository _appRepository;

        public PermissionController(
            ILogger<ProfileController> logger,
            IPermissionRepository permissionRepository,
            IAppRepository appRepository)
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
            _appRepository = appRepository;
        }

        [AuthorizeRole(RoleConstants.PermissionRole.List)]
        public async Task<IActionResult> IndexAsync(PermissionPaginatedRequest? request)
        {
            try
            {
                request.PageSize = 1000;
                var app = await _appRepository.GetByIdAsync(request.AppId);  
                var profiles = await _permissionRepository.GetAsync(request);
                return View(new AppPermissionViewModel {
                    App = app,
                    Permissions = profiles 
                });

            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                return View(new PaginatedResponse<PermissionResponse>(){
                    Request = new PaginatedRequest(),
                });
            }
        }
    }
}