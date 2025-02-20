using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Repositories;
using Family.Accounts.Management.Infrastructure.Requests;
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
                request.PageSize = 2;
                var apps = await _appRepository.GetAsync(request);
                return View(apps);

            }
            catch(Exception ex){
                var teste = ex.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}