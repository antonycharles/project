using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Accounts.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Accounts.Management.Infrastructure.Settings;

namespace Accounts.Management.Web.Controllers;

[Authorize]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ILogger<HomeController> logger,
        IOptions<AccountsManagementSettings> configuration) : base(configuration)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var teste = User.Claims.ToList();
        return View();
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
