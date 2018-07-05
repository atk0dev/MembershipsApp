using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    using System.Text;

    public class EditButtonModel
    {
        public int ItemId { get; set; }

        public int ProductId { get; set; }
        
        public int SubscriptionId { get; set; }

        public string Link {
            get
            {
                var s = new StringBuilder("?");

                if (this.ItemId > 0)
                {
                    s.Append(string.Format("{0}={1}&", "itemid", this.ItemId));
                }

                if (this.ProductId > 0)
                {
                    s.Append(string.Format("{0}={1}&", "productid", this.ProductId));
                }

                if (this.SubscriptionId > 0)
                {
                    s.Append(string.Format("{0}={1}&", "subscriptionid", this.SubscriptionId));
                }

                return s.ToString().Substring(0, s.Length - 1);
            }
        }
    }
}