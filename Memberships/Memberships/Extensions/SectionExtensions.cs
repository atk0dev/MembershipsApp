using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extensions
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Memberships.Comparers;
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

            var result = sections.Distinct(new ProductSectionEqualityComparer()).ToList();
            var model = new ProductSectionModel
                        {
                            Title = await (from p in db.Products where p.Id.Equals(productId) select p.Title).FirstOrDefaultAsync(),
                            Sections = result
                        };

            return model;
        }
    }
}