using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Repositories;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Accounts.Management.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Management.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _clientRepository;

        public ClientController(
            ILogger<ClientController> logger,
            IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.List)]
        public async Task<IActionResult> IndexAsync(PaginatedRequest? request)
        {
            try
            {
                var users = await _clientRepository.GetAsync(request);
                return View(users);

            }
            catch(ExternalApiException ex){
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch(Exception ex){
                return View(new PaginatedResponse<ClientResponse>(){
                    Request = new PaginatedRequest(),
                });
            }
        }


        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.List)]
        public async Task<IActionResult> DetailsAsync(Guid id){
            try
            {
                var user = await _clientRepository.GetByIdAsync(id);
                return View(user);
            }
            catch(Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}