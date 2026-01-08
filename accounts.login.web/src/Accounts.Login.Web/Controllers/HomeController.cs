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
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAppRepository _appRepository;
    private readonly ICompanyRepository _companyRepository;

    public HomeController(
        ILogger<HomeController> logger, 
        IAppRepository appRepository, 
        ICompanyRepository companyRepository,
        IOptions<AccountsLoginSettings> configuration) : base(configuration)
    {
        _logger = logger;
        _appRepository = appRepository;
        _companyRepository = companyRepository;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var appsPublic = await _appRepository.GetPublicAppsByUserIdAsync(User.GetId());
        var companies = await _companyRepository.GetCompaniesByUserIdAsync(User.GetId());
        return View(new HomeViewModel {
            Apps = appsPublic.ToList(),
            Companies = companies.ToList()
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
