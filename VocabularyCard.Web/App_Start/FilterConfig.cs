using System.Web;
using System.Web.Mvc;
using VocabularyCard.Web.Filters;

namespace VocabularyCard.Web
{
    public class FilterConfig
    {
        // 註冊全域 exception filter
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogOutputAttribute());
        }
    }
}
