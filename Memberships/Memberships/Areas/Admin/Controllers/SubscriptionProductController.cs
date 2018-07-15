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
    using Memberships.Constants;

    [Authorize(Roles = UserRoleValues.AdminRoleName)]
    public class SubscriptionProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SubscriptionProduct
        public async Task<ActionResult> Index()
        {
            return this.View(await this.db.SubscriptionProducts.Convert(this.db));
        }

        // GET: Admin/SubscriptionProduct/Details/5
        public async Task<ActionResult> Details(int? subscriptionId, int? productId)
        {
            if (subscriptionId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubscriptionProduct subscriptionProduct = await this.GetSubscriptionProduct(subscriptionId, productId);
            if (subscriptionProduct == null)
            {
                return this.HttpNotFound();
            }

            return this.View(await subscriptionProduct.Convert(this.db));
        }

        // GET: Admin/SubscriptionProduct/Create
        public async Task<ActionResult> Create()
        {
            var model = new SubscriptionProductModel
                        {
                            Subscriptions = await this.db.Subscriptions.ToListAsync(),
                            Products = await this.db.Products.ToListAsync()
                        };

            return this.View(model);
        }

        // POST: Admin/SubscriptionProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,SubscriptionId")] SubscriptionProduct subscriptionProduct)
        {
            if (this.ModelState.IsValid)
            {
                this.db.SubscriptionProducts.Add(subscriptionProduct);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(subscriptionProduct);
        }

        // GET: Admin/SubscriptionProduct/Edit/5
        public async Task<ActionResult> Edit(int? subscriptionId, int? productId)
        {
            if (subscriptionId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubscriptionProduct subscriptionProduct = await this.GetSubscriptionProduct(subscriptionId, productId);
            if (subscriptionProduct == null)
            {
                return this.HttpNotFound();
            }

            return this.View(await subscriptionProduct.Convert(this.db));
        }

        // POST: Admin/SubscriptionProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,SubscriptionId,OldProductId,OldSubscriptionId")] SubscriptionProduct subscriptionProduct)
        {
            if (this.ModelState.IsValid)
            {
                var canChange = await subscriptionProduct.CanChange(this.db);
                if (canChange)
                {
                    await subscriptionProduct.Change(this.db);
                }

                return this.RedirectToAction("Index");
            }

            return this.View(await subscriptionProduct.Convert(this.db));
        }

        // GET: Admin/SubscriptionProduct/Delete/5
        public async Task<ActionResult> Delete(int? subscriptionId, int? productId)
        {
            if (subscriptionId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubscriptionProduct subscriptionProduct = await this.GetSubscriptionProduct(subscriptionId, productId);
            if (subscriptionProduct == null)
            {
                return this.HttpNotFound();
            }

            return this.View(await subscriptionProduct.Convert(this.db));
        }

        // POST: Admin/SubscriptionProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? subscriptionId, int? productId)
        {
            SubscriptionProduct subscriptionProduct = await this.GetSubscriptionProduct(subscriptionId, productId);
            this.db.SubscriptionProducts.Remove(subscriptionProduct);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        private async Task<SubscriptionProduct> GetSubscriptionProduct(int? subscriptionId, int? productId)
        {
            try
            {
                int ssrId = 0;
                int prdId = 0;

                int.TryParse(subscriptionId.ToString(), out ssrId);
                int.TryParse(productId.ToString(), out prdId);

                var subscriptionProduct =
                    await this.db.SubscriptionProducts.FirstOrDefaultAsync(
                        sp => sp.ProductId.Equals(prdId) && sp.SubscriptionId.Equals(ssrId));

                return subscriptionProduct;
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
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
