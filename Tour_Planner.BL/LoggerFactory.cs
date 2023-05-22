using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Tour_Planner.BL {
    public class LoggerFactory {
        public static ILoggerWrapper GetLogger(Type type) {
            log4net.ILog logger = log4net.LogManager.GetLogger(type);
            return new LoggerWrapper(logger);
        }
    }
}
