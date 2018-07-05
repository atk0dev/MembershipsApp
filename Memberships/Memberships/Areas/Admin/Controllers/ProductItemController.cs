using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Memberships.Entities;
using Memberships.Models;

namespace Memberships.Areas.Admin.Controllers
{
    using Memberships.Areas.Admin.Models;

    public class ProductItemController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductItem
        public async Task<ActionResult> Index()
        {
            return this.View(await this.db.ProductItems.ToListAsync());
        }

        // GET: Admin/ProductItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // GET: Admin/ProductItem/Create
        public async Task<ActionResult> Create()
        {
            var model = new ProductItemModel
                        {
                            Items = await this.db.Items.ToListAsync(),
                            Products = await this.db.Products.ToListAsync()
                        };

            return this.View(model);
        }

        // POST: Admin/ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,ItemId")] ProductItem productItem)
        {
            if (this.ModelState.IsValid)
            {
                this.db.ProductItems.Add(productItem);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(productItem);
        }

        // GET: Admin/ProductItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ItemId")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productItem);
        }

        // GET: Admin/ProductItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            db.ProductItems.Remove(productItem);
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
