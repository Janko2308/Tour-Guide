using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Tour_Planner.BL {
    public class LoggerFactory {

        public static ILoggerWrapper GetLogger()
        {
            StackTrace stackTrace = new StackTrace(1, false); //Captures 1 frame, false for not collecting information about the file
            var type = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            return LoggerWrapper.CreateLogger("./log4net.config", type.FullName);
        }
    }
}
