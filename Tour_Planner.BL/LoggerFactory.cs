using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Tour_Planner.BL {
    public class LoggerFactory {
        
        private static bool isConfigured = false;
        public static ILoggerWrapper GetLogger(Type type) {
            if (!isConfigured) {
                XmlConfigurator.Configure();
                isConfigured = true;
            }
            
            ILog logger = LogManager.GetLogger(type);
            return new LoggerWrapper(logger);
        }
    }
}
