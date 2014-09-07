using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC5.Helper
{
    public class Html
    {
        public static MvcHtmlString StaticMapImage(string longitude, string latitude, int width, int height, int zoom, string color, string label)
        {
            StringBuilder builder = new StringBuilder();

            //Prev
            var imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", "http://maps.googleapis.com/maps/api/staticmap?" +
            "zoom=" + zoom +
            "&size=" + width + "x" + height +
            "&maptype=roadmap" +
                "&markers=color:" + color +
                "|label:" + label +
                    "|" + latitude + "," + longitude);
            imgTagBuilder.MergeAttribute("height", height.ToString());
            imgTagBuilder.MergeAttribute("width", width.ToString());
            imgTagBuilder.AddCssClass("static_map");
            builder.AppendLine(imgTagBuilder.ToString());
            return new MvcHtmlString(builder.ToString());
        }
    }
}