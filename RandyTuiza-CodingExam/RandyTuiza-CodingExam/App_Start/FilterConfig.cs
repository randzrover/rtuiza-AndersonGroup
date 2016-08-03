using System.Web;
using System.Web.Mvc;

namespace RandyTuiza_CodingExam
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
