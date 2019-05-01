using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotbollstips.Logic
{
    public class Log4NetLogic
    {
        private static readonly log4net.ILog logInfo = LogManager.GetLogger("InfoLogger");
        private static readonly log4net.ILog logError = LogManager.GetLogger("ErrorLogger");

        public static void Log(LogLevel level, string logText, string caller)
        {
            Log(level, logText, caller, null);
        }

        public static void Log(LogLevel level, string logText, string caller, Exception e)
        {
            string textToLog = BuildLogText(logText, caller, e);

            if (level == LogLevel.INFO)
            {
                logInfo.Info(textToLog);
            }
            else if (level == LogLevel.ERROR)
            {
                logError.Info(textToLog);
            }
        }

        private static string BuildLogText(string logText, string caller, Exception e)
        {
            string returnString = "";

            returnString += string.Format("Caller: {0}. Text: {1}. \n\n", caller, logText);

            if (e != null)
            {
                string inner = e.InnerException != null ? e.InnerException.ToString() : "NULL";

                returnString += string.Format("Exception: {0} Inner exception: {1}", e.Message.ToString(), inner);
            }

            return returnString;
        }

        public enum LogLevel
        {
            INFO,
            ERROR
        }
    }
}