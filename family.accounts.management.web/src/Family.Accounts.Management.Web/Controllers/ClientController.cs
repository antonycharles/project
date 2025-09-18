using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Repositories;
using Family.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Family.Accounts.Management.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Family.Accounts.Management.Web.Controllers
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