using System.Web;
using System.Web.Mvc;
using Twitler.Filters;

namespace Twitler
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilterAttribute());
        }
    }
}
