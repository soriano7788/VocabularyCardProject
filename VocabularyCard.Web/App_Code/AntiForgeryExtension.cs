using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

public class AntiForgeryExtension : IAntiForgeryAdditionalDataProvider
{
    public string GetAdditionalData(HttpContextBase context)
    {
        return DateTime.UtcNow.Ticks.ToString();
    }
    public bool ValidateAdditionalData(HttpContextBase context, string additionalData)
    {
        if (string.IsNullOrWhiteSpace(additionalData))
        {
            return false;
        }

        var requestTime = Convert.ToInt64(additionalData);
        var now = DateTime.UtcNow.Ticks;
        var difference = new TimeSpan(now - requestTime);

        // 10 分鐘內都算有效
        return (difference.TotalMinutes > -1 && difference.TotalMinutes < 10);
    }
}
