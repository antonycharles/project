using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Settings;
using Accounts.Login.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Web.Controllers
{
    public abstract class BaseHomeController : BaseController
    {
        private readonly ICompanyRepository _companyRepository;

        public BaseHomeController(IOptions<AccountsLoginSettings> configuration, ICompanyRepository companyRepository)
            : base(configuration)
        {
            _companyRepository = companyRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (User.Identity?.IsAuthenticated == true)
            {
                ViewData["Companies"] = _companyRepository.GetCompaniesByUserIdAsync(User.GetId()).GetAwaiter().GetResult().ToList();
            }
        }
    }
}
