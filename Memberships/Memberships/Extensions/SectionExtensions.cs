using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extensions
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Memberships.Comparers;
    using Memberships.Entities;
    using Memberships.Models;

    public static class SectionExtensions
    {
        public static async Task<ProductSectionModel> GetProductSectionsAsync(int productId, string userId)
        {
            var db = ApplicationDbContext.Create();
            var sections = await (from p in db.Products
                    join pi in db.ProductItems on p.Id equals pi.ProductId
                    join i in db.Items on pi.ItemId equals i.Id
                    join s in db.Sections on i.SectionId equals s.Id
                    where p.Id.Equals(productId)
                    orderby s.Title
                    select new ProductSection
                                     {
                                         Id = s.Id,
                                         ItemTypeId = i.ItemTypeId,
                                         Title = s.Title
                                     }).ToListAsync();

            foreach (var section in sections)
            {
                section.Items = await GetProductItemRowsAsync(productId, section.Id, section.ItemTypeId, userId);
            }

            var result = sections.Distinct(new ProductSectionEqualityComparer()).ToList();

            // place download section last
            var union = result.Where(r => !r.Title.ToLower().Contains("download"))
                .Union(result.Where(r => r.Title.ToLower().Contains("download")));

            var model = new ProductSectionModel
                        {
                            Title = await (from p in db.Products where p.Id.Equals(productId) select p.Title).FirstOrDefaultAsync(),
                            Sections = union
                        };

            return model;
        }

        public static async Task<IEnumerable<ProductItemRow>> GetProductItemRowsAsync(
            int productId,
            int sectionId,
            int itemTypeId,
            string userId,
            ApplicationDbContext db = null)
        {
            if (db == null)
            {
                db = ApplicationDbContext.Create();
            }

            var today = DateTime.Now.Date;

            var items = await (from i in db.Items
                    join it in db.ItemTypes on i.ItemTypeId equals it.Id
                    join pi in db.ProductItems on i.Id equals pi.ItemId
                    join sp in db.SubscriptionProducts on pi.ProductId equals sp.ProductId
                    join us in db.UserSubscriptions on sp.SubscriptionId equals us.SubscriptionId
                               where i.SectionId.Equals(sectionId) && pi.ProductId.Equals(productId) && us.UserId.Equals(userId)
                                     orderby i.PartId
                               select new ProductItemRow
                                      {
                                          ItemId = i.Id,
                                          Description = i.Description,
                                          Title = i.Title,
                                          Link = it.Title.Equals("Download") ? i.Url : "/productcontent/content/" + pi.ProductId.ToString() + "/" + i.Id.ToString(),
                                          ImageUrl = i.ImageUrl,
                                          ReleaseDate = DbFunctions.CreateDateTime(us.StartDate.Value.Year, us.StartDate.Value.Month, us.StartDate.Value.Day + i.WaitDays, 0, 0, 0),
                                          IsAvailable = DbFunctions.CreateDateTime(today.Year, today.Month, today.Day, 0, 0, 0) >= DbFunctions.CreateDateTime(us.StartDate.Value.Year, us.StartDate.Value.Month, us.StartDate.Value.Day + i.WaitDays, 0, 0, 0),
                                          IsDownload = it.Title.Equals("Download")
                               }).ToListAsync();
            return items;
        }

        public static async Task<ContentViewModel> GetContentAsync(int productId, int itemId)
        {
            var db = ApplicationDbContext.Create();
            return await (from i in db.Items
                    join it in db.ItemTypes on i.ItemTypeId equals it.Id
                          where i.Id.Equals(itemId)
                          select new ContentViewModel
                                 {
                                     ProductId = productId,
                                     Html = i.Html,
                                     Title = i.Title,
                                     VideoUrl = i.Url,
                                     Description = i.Description
                                 }).FirstOrDefaultAsync();
        }
    }
}