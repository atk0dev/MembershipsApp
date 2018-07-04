using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    using System.Text;

    public class SmallButtonModel
    {
        public string Action { get; set; }

        public string Text { get; set; }

        public string Glyph { get; set; }

        public string ButtonType { get; set; }

        public int? Id { get; set; }

        public int? ItemId { get; set; }

        public int? ProductId { get; set; }

        public int? SubscriptionId { get; set; }
        
        public string ActionParameters {
            get
            {
                var param = new StringBuilder("?");

                this.AddStringParam(param, "id", this.Id);
                this.AddStringParam(param, "itemId", this.ItemId);
                this.AddStringParam(param, "productId", this.ProductId);
                this.AddStringParam(param, "subscriptionId", this.SubscriptionId);

                if (param.Length > 0)
                {
                    return param.ToString().Substring(0, param.Length - 1);
                }

                return string.Empty;
            }
        }

        private void AddStringParam(StringBuilder param, string name, int? value)
        {
            if (value != null && value > 0)
            {
                param.AppendFormat("{0}={1}&", name, value);
            }
        }
    }
}