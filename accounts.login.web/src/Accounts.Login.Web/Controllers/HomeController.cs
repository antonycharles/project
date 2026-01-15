using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Accounts.Login.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Web.Extensions;
using Accounts.Login.Infra.Settings;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Web.Controllers;

[Authorize]
public class HomeController : BaseHomeController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAppRepository _appRepository;

    public HomeController(
        ILogger<HomeController> logger, 
        IAppRepository appRepository, 
        ICompanyRepository companyRepository,
        IOptions<AccountsLoginSettings> configuration) : base(configuration,companyRepository)
    {
        _logger = logger;
        _appRepository = appRepository;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var appsPublic = await _appRepository.GetPublicAppsByUserIdAsync(User.GetId());
        return View(new HomeViewModel {
            Apps = appsPublic.ToList()
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
