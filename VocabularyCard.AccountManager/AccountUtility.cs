using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.AccountManager
{
    public class AccountUtility
    {
        public static readonly Logger Logger = LogManager.GetLogger("AccountManager");
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
    }
}
