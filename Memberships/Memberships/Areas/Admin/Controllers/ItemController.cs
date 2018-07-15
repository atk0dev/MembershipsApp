using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Memberships.Constants;
using Memberships.Entities;
using Memberships.Models;

namespace Memberships.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRoleValues.AdminRoleName)]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return this.View(await this.db.Items.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Create()
        {
            var model = new Item
                        {
                            ItemTypes = this.db.ItemTypes.ToList(),
                            Parts = this.db.Parts.ToList(),
                            Sections = this.db.Sections.ToList()
                        };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Url,ImageUrl,Html,WaitDays,ProductId,ItemTypeId,SectionId,PartId,IsFree")] Item item)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Items.Add(item);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(item);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await this.db.Items.FindAsync(id);
            if (item == null)
            {
                return this.HttpNotFound();
            }

            item.ItemTypes = await this.db.ItemTypes.ToListAsync();
            item.Parts = await this.db.Parts.ToListAsync();
            item.Sections = await this.db.Sections.ToListAsync();

            return this.View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Url,ImageUrl,Html,WaitDays,ProductId,ItemTypeId,SectionId,PartId,IsFree")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
