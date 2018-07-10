using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Comparers
{
    using Memberships.Models;

    public class ProductSectionEqualityComparer : IEqualityComparer<ProductSection>
    {
        public bool Equals(ProductSection productSection1, ProductSection productSection2)
        {
            if (productSection1 == null)
            {
                return false;
            }

            if (productSection2 == null)
            {
                return false;
            }

            return productSection1.Id.Equals(productSection2.Id);
        }

        public int GetHashCode(ProductSection productSection)
        {
            return productSection.Id.GetHashCode();
        }
    }
}