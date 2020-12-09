using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Core
{

    public interface ILogger
    {
        void Log(LogLevel logLevel, string content);
    }


    public enum LogLevel
    {
        None = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
    }
}
