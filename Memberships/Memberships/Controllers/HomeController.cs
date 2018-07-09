using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Memberships.Controllers
{
    using System.Threading.Tasks;

    using Memberships.Extensions;
    using Memberships.Models;

    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var userId = this.Request.IsAuthenticated ? this.HttpContext.User.Identity.GetUserId() : null;
            var thumbnails = await new List<ThumbnailModel>().GetProductThumbnailAsync(userId);
            var count = thumbnails.Count() / 4;
            var model = new List<ThumbnailAreaModel>();
            for (int i = 0; i <= count; i++)
            {
                model.Add(new ThumbnailAreaModel
                          {
                              Title = i.Equals(0) ? "My content" : string.Empty,
                              Thumbnails = thumbnails.Skip(i * 4).Take(4)
                          });
            }

            return this.View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}