using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Models
{
    public class ProductSectionModel
    {
        public string Title { get; set; }

        public IEnumerable<ProductSection> Sections { get; set; }
    }
}