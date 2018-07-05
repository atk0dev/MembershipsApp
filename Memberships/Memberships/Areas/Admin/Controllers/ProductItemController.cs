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

    public class ProductItemController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductItem
        public async Task<ActionResult> Index()
        {
            return this.View(await this.db.ProductItems.Convert(this.db));
        }

        // GET: Admin/ProductItem/Details/5
        public async Task<ActionResult> Details(int? itemId, int? productId)
        {
            if (itemId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductItem productItem = await this.GetProductItem(itemId, productId);
            if (productItem == null)
            {
                return this.HttpNotFound();
            }
            return this.View(await productItem.Convert(this.db));
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
        public async Task<ActionResult> Edit(int? itemId, int? productId)
        {
            if (itemId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await this.GetProductItem(itemId, productId);
            if (productItem == null)
            {
                return this.HttpNotFound();
            }
            return this.View(await productItem.Convert(this.db));
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ItemId,OldProductId,OldItemId")] ProductItem productItem)
        {
            if (this.ModelState.IsValid)
            {
                var canChange = await productItem.CanChange(this.db);
                if (canChange)
                {
                    await productItem.Change(this.db);
                }

                return this.RedirectToAction("Index");
            }
            return this.View(await productItem.Convert(this.db));
        }

        // GET: Admin/ProductItem/Delete/5
        public async Task<ActionResult> Delete(int? itemId, int? productId)
        {
            if (itemId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductItem productItem = await this.GetProductItem(itemId, productId);
            if (productItem == null)
            {
                return this.HttpNotFound();
            }
            return this.View(await productItem.Convert(this.db));
        }

        // POST: Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int itemId, int productId)
        {
            ProductItem productItem = await this.GetProductItem(itemId, productId);
            this.db.ProductItems.Remove(productItem);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        private async Task<ProductItem> GetProductItem(int? itemId, int? productId)
        {
            try
            {
                int itmId = 0;
                int prdId = 0;

                int.TryParse(itemId.ToString(), out itmId);
                int.TryParse(productId.ToString(), out prdId);

                var productItem =
                    await this.db.ProductItems.FirstOrDefaultAsync(
                        pi => pi.ProductId.Equals(prdId) && pi.ItemId.Equals(itmId));

                return productItem;
            }
            catch
            {
                return null;
            }
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
