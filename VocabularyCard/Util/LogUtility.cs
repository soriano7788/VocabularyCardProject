using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace VocabularyCard.Util
{
    public class LogUtility
    {
        public static readonly Logger Logger = LogManager.GetLogger("BASE");

        public static void ErrorLog(string message)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(message);
            }
        }
        public static void DebugLog(string message)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(message);
            }
        }
        public static void InfoLog(string message)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info(message);
            }
        }

        public static string GetExceptionDetails(Exception ex)
        {
            Exception logException = ex;
            if(ex.InnerException != null)
            {
                logException = ex.InnerException;
            }
            
            StringBuilder message = new StringBuilder();
            message.AppendLine();
            // 需要 system.web，此 library 應該不需要知道這是 web 環境
            // 不然就是獨立一個 lib 實作 interface 表明是 impl web，裡面再來用 system.web
            //message.AppendLine("要求虛擬路徑: " + );




            return message.ToString();
        }
    }
}
