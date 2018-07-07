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
    using Memberships.Areas.Admin.Extensions;
    using Memberships.Areas.Admin.Models;

    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        public async Task<ActionResult> Index()
        {
            var products = await this.db.Products.ToListAsync();
            var model = await products.Convert(this.db);
            return this.View(model);
        }

        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await this.db.Products.FindAsync(id);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            var model = await product.Convert(this.db);

            return this.View(model);
        }

        // GET: Admin/Product/Create
        public async Task<ActionResult> Create()
        {
            var model = new ProductModel
                        {
                            ProductLinkTexts = await this.db.ProductLinkTexts.ToListAsync(),
                            ProductTypes = await this.db.ProductTypes.ToListAsync()
                        };

            return this.View(model);
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImageUrl,ProductLinkTextId,ProductTypeId")] Product product)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Products.Add(product);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            var model = await product.Convert(this.db);
            return this.View(model);
        }

        // GET: Admin/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await this.db.Products.FindAsync(id);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            var prod = new List<Product> { product };
            var prodModel = await prod.Convert(this.db);

            return this.View(prodModel.First());
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ImageUrl,ProductLinkTextId,ProductTypeId")] Product product)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(product).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            var model = await product.Convert(this.db);
            return this.View(model);
        }

        // GET: Admin/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await this.db.Products.FindAsync(id);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            var model = await product.Convert(this.db);
            return this.View(model);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await this.db.Products.FindAsync(id);
            this.db.Products.Remove(product);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
