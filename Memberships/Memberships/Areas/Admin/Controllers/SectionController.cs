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
    public class SectionController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Section
        public async Task<ActionResult> Index()
        {
            return this.View(await this.db.Sections.ToListAsync());
        }

        // GET: Admin/Section/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await this.db.Sections.FindAsync(id);
            if (section == null)
            {
                return this.HttpNotFound();
            }
            return this.View(section);
        }

        // GET: Admin/Section/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Admin/Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] Section section)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Sections.Add(section);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(section);
        }

        // GET: Admin/Section/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await this.db.Sections.FindAsync(id);
            if (section == null)
            {
                return this.HttpNotFound();
            }
            return this.View(section);
        }

        // POST: Admin/Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] Section section)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(section).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }
            return this.View(section);
        }

        // GET: Admin/Section/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await this.db.Sections.FindAsync(id);
            if (section == null)
            {
                return this.HttpNotFound();
            }
            return this.View(section);
        }

        // POST: Admin/Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Section section = await this.db.Sections.FindAsync(id);
            this.db.Sections.Remove(section);
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
