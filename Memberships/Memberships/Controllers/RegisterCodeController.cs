using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memberships.Controllers
{
    using System.Threading.Tasks;

    using Memberships.Extensions;

    public class RegisterCodeController : Controller
    {
        public async Task<ActionResult> Register(string code)
        {
            if (this.Request.IsAuthenticated)
            {
                var userId = this.HttpContext.GetUserId();
                var registered = await SubscriptionExtensions.RegisterUserSubscriptionCode(userId, code);
                if (!registered)
                {
                    throw new ApplicationException();
                }

                return this.PartialView("_RegisterCodePartial");
            }

            return this.View();
        }
    }
}