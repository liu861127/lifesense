using System.Web;
using System.Web.Mvc;

namespace lifesenseSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AppHandleErrorAttribute());
        }
    }
}