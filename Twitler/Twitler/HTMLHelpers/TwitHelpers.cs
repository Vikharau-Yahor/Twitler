using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twitler.HTMLHelpers
{
    public static class TwitHelpers
    {
        public static MvcHtmlString RenderRemoveTwitBtn(this HtmlHelper html, bool canModified, int twitId)
        {
            MvcHtmlString result = new MvcHtmlString("");
            if (canModified)
            {
                 result = new MvcHtmlString($"<button class='close btn-delete-twit' data-twit-id={twitId} data-dismiss='modal' area-hidden='true'>x</button>");
            }

            return result;
        }
    }
}