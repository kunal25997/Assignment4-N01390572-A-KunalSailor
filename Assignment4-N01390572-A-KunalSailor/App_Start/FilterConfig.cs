using System.Web;
using System.Web.Mvc;

namespace Assignment4_N01390572_A_KunalSailor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
