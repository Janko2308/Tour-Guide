using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.BL {
    public interface ILoggerWrapper {
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Warn(string message);
        void Fatal(string message, Exception ex);
    }
}
