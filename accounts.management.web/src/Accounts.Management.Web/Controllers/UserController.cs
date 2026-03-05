using Accounts.Management.Infrastructure.Enums;
using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Mappers;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Accounts.Management.Infrastructure.Settings;
using Accounts.Management.Web.Extensions;
using Accounts.Management.Web.Helpers;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Management.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserController(
            IUserRepository userRepository,
            IProfileRepository profileRepository,
            IOptions<AccountsManagementSettings> settings,
            IUserProfileRepository userProfileRepository) : base(settings)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.UserRole.List)]
        public async Task<IActionResult> IndexAsync(PaginatedRequest? request)
        {
            try
            {
                request ??= new PaginatedRequest();
                request.CompanyId = User.CompanyId();
                var users = await _userRepository.GetAsync(request);
                return View(users);
            }
            catch (ExternalApiException ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch
            {
                return View(new PaginatedResponse<UserResponse>
                {
                    Request = new PaginatedRequest(),
                });
            }
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.UserRole.List)]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id, User.CompanyId());
                var profiles = await _profileRepository.GetAsync(new ProfilePaginatedRequest
                {
                    PageSize = 1000,
                    Type = ProfileTypeEnum.User,
                });

                return View(new UserDetailsViewModel
                {
                    User = user,
                    AvailableProfiles = profiles.Items
                });
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserProfileRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProfileAsync(Guid userId, Guid profileId)
        {
            try
            {
                await _userProfileRepository.CreateAsync(new UserProfileRequest
                {
                    UserId = userId,
                    ProfileId = profileId,
                    CompanyId = User.CompanyId()
                });

                HttpContext.AddMessageSuccess("Profile added to user.");
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
            }

            return RedirectToAction("Details", new { id = userId });
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserProfileRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfileStatusAsync(Guid userId, Guid userProfileId, Guid profileId, src.Accounts.Management.Infrastructure.Enums.StatusEnum status)
        {
            try
            {
                await _userProfileRepository.UpdateAsync(userProfileId, new UserProfileUpdateRequest
                {
                    Id = userProfileId,
                    ProfileId = profileId,
                    Status = status
                });

                HttpContext.AddMessageSuccess("User profile updated.");
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
            }

            return RedirectToAction("Details", new { id = userId });
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserProfileRole.Delete)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfileAsync(Guid userId, Guid userProfileId)
        {
            try
            {
                await _userProfileRepository.DeleteAsync(userProfileId);
                HttpContext.AddMessageSuccess("Profile removed from user.");
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
            }

            return RedirectToAction("Details", new { id = userId });
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.UserRole.Create)]
        public IActionResult CreateAsync()
        {
            return View(new UserRequest());
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserRole.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(UserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                await _userRepository.CreateAsync(request);
                HttpContext.AddMessageSuccess("User created success!");
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
        [AuthorizeRole(RoleConstants.UserRole.Update)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id, User.CompanyId());
                return View(user.ToUserUpdateRequest());
            }
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserRole.Update)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, UserUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                await _userRepository.UpdateAsync(id, request);

                HttpContext.AddMessageSuccess("User update success!");
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
        [AuthorizeRole(RoleConstants.UserRole.Delete)]
        public async Task<IActionResult> DeleteConfirmAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id, User.CompanyId());
                return View(user);
            }
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
                HttpContext.AddMessageError(ex.Message);
                return RedirectToAction("DeleteConfirmAsync", new { id });
            }
        }
    }
}
