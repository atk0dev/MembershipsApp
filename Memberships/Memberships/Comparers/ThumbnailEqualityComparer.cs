using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Comparers
{
    using Memberships.Models;
    public class ThumbnailEqualityComparer : IEqualityComparer<ThumbnailModel>
    {
        public bool Equals(ThumbnailModel thumbnail1, ThumbnailModel thumbnail2)
        {
            if (thumbnail1 == null)
            {
                return false;
            }

            if (thumbnail2 == null)
            {
                return false;
            }

            return thumbnail1.ProductId.Equals(thumbnail2.ProductId);
        }

        public int GetHashCode(ThumbnailModel thumbnail)
        {
            return thumbnail.ProductId;
        }
    }
}