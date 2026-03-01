using Accounts.Management.Infrastructure.Enums;
using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Accounts.Management.Web.Helpers;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IClientProfileRepository _clientProfileRepository;

        public ClientController(
            IClientRepository clientRepository,
            IProfileRepository profileRepository,
            IClientProfileRepository clientProfileRepository)
        {
            _clientRepository = clientRepository;
            _profileRepository = profileRepository;
            _clientProfileRepository = clientProfileRepository;
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.List)]
        public async Task<IActionResult> IndexAsync(PaginatedRequest? request)
        {
            try
            {
                var clients = await _clientRepository.GetAsync(request);
                return View(clients);

            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch
            {
                return View(new PaginatedResponse<ClientResponse>
                {
                    Request = new PaginatedRequest(),
                });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.List)]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                var profiles = await _profileRepository.GetAsync(new ProfilePaginatedRequest
                {
                    PageSize = 1000,
                    Type = ProfileTypeEnum.System
                });

                return View(new ClientDetailsViewModel
                {
                    Client = client,
                    AvailableProfiles = profiles.Items
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.Create)]
        public IActionResult CreateAsync()
        {
            return View(new ClientRequest());
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ClientRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ClientRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                await _clientRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("Client created success!");
                return RedirectToAction("Index");
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                return View(new ClientUpdateRequest
                {
                    Name = client.Name,
                    Status = client.Status
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ClientRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, ClientUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                await _clientRepository.UpdateAsync(id, request);
                HttpContext.AddMessageSuccess("Client update success!");
                return RedirectToAction("Index");
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                return View(client);
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ClientRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _clientRepository.DeleteAsync(id);
                HttpContext.AddMessageSuccess("Client delete success!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("DeleteConfirmAsync", new { id });
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.ClientProfileRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProfileAsync(Guid clientId, Guid profileId)
        {
            try
            {
                await _clientProfileRepository.CreateAsync(new ClientProfileRequest
                {
                    ClientId = clientId,
                    ProfileId = profileId
                });
                HttpContext.AddMessageSuccess("Profile added to client.");
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
            }

            return RedirectToAction("Details", new { id = clientId });
        }

    }
}
