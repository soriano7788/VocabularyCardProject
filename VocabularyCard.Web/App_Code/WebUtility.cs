using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using VocabularyCard;
using VocabularyCard.Web.Extensions;

public class WebUtility
{
    private static RepositoryFactory _repository;
    public static RepositoryFactory Repository
    {
        get
        {
            if (_repository == null)
            {
                _repository = new RepositoryFactory();
            }
            return _repository;
        }
    }

    public static string SafeHtml(string html)
    {
        string filterString = string.IsNullOrEmpty(html) ? string.Empty : html.Trim();
        if (string.IsNullOrEmpty(filterString))
        {
            return null;
        }
        // 只有 br、p、em、span、style 這些 html 標籤會保留，
        // 其餘的 html 標籤會被拿掉
        // e.g. <script>zzz</script> 會剩下 zzz
        Regex regex = new Regex(@"<(?!br|\/?p|style|\/?span)[^>]*>");
        filterString = regex.Replace(filterString, "");

        return filterString;
    }
    public static string GetUserData()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = id.Ticket;
            var userInfo = id.Ticket.UserData;
            return userInfo;
        }
        return string.Empty;
    }

    //public static T TestNewFunctionality<T>(T t) : where new T()
    //{
    //    return new T();
    //}

    public static AjaxJsonResult ReturnAjaxResult(bool success, object data)
    {
        return new AjaxJsonResult(success, data);
    }
}
