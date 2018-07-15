using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memberships.Controllers
{
    using System.IO;

    [Authorize(Roles = "admin")]
    public class FileController : Controller
    {
        public ActionResult Index()
        {
            var path = this.Server.MapPath("~/Content/Files/");
            var dir = new DirectoryInfo(path);
            var files = dir.EnumerateFiles().Select(f => f.Name);
            return this.View(files);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var path = Path.Combine(this.Server.MapPath("~/Content/Files/"), file.FileName);
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            using (var sw = new FileStream(path, FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }
            return this.RedirectToAction("Index");
        }
    }
}