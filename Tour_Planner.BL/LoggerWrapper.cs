using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.BL {
    public class LoggerWrapper : ILoggerWrapper {
        private readonly log4net.ILog _log;

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
