using System.Web;
using System.Web.Mvc;

namespace Exer2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); // Add this line to apply authorization globally
        }
    }
}
