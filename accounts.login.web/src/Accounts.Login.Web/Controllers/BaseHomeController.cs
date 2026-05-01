using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Settings;
using Accounts.Login.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Web.Controllers
{
    public abstract class BaseHomeController : BaseController
    {

        public BaseHomeController(IOptions<AccountsLoginSettings> configuration)
            : base(configuration)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (User.Identity?.IsAuthenticated == true)
            {
            }
        }
    }
}
