using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memberships.Controllers
{
    using System.Threading.Tasks;

    using Memberships.Extensions;
    using Memberships.Models;

    [Authorize]
    public class ProductContentController : Controller
    {
        // GET: ProductContent
        public async Task<ActionResult> Index(int id)
        {
            var userId = this.Request.IsAuthenticated ? this.HttpContext.GetUserId() : null;
            var sections = await SectionExtensions.GetProductSectionsAsync(id, userId);
            return this.View(sections);
        }
    }
}