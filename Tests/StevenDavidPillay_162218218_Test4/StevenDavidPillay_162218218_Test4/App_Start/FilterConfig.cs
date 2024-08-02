using System.Web;
using System.Web.Mvc;

namespace StevenDavidPillay_162218218_Test4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
