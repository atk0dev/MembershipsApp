﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extensions
{
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString GlyphLink(
            this HtmlHelper htmlHelper,
            string controller,
            string action,
            string text,
            string glyphIcon,
            string cssClasses = "",
            string id = "",
            Dictionary<string, string> attributes = null)
        {
            var glyph = string.Format("<span class='glyphicon glyphicon-{0}'></span>", glyphIcon);

            var anchor = new TagBuilder("a");

            if (!string.IsNullOrWhiteSpace(controller))
            {
                anchor.MergeAttribute("href", string.Format("/{0}/{1}/", controller, action));
            }
            else
            {
                anchor.MergeAttribute("href", "#");
            }

            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    anchor.MergeAttribute(attribute.Key, attribute.Value);
                }
            }

            anchor.InnerHtml = string.Format("{0} {1}", glyph, text);
            anchor.AddCssClass(cssClasses);
            anchor.GenerateId(id);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }
    }
}