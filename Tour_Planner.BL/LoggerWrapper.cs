using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.BL {
    public class LoggerWrapper : ILoggerWrapper {
        private readonly log4net.ILog _log;

        public static LoggerWrapper CreateLogger(string configPath, string caller)
        {
            if (!File.Exists(configPath))
            {
                throw new ArgumentException("Does not exist.", nameof(configPath));
            }

            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            var logger = log4net.LogManager.GetLogger(caller);  // System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            return new LoggerWrapper(logger);
        }
        
        public LoggerWrapper(log4net.ILog log) {
            _log = log;
        }

        public void Debug(string message) {
            _log.Debug(message);
        }

        public void Error(string message) {
            _log.Error(message);
        }

        public void Info(string message) {
            _log.Info(message);
        }

        public void Warn(string message) {
            _log.Warn(message);
        }

        public void Fatal(string message, Exception ex) {
            _log.Fatal(message, ex);
        }
    }
}
