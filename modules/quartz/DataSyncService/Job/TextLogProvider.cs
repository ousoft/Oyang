using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Job
{
    public class TextLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return Logger;
        }

        public bool Logger(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            if (logLevel >= LogLevel.Info && messageFunc != null)
            {
                var now = DateTime.Now;
                var path = $@"logs/{now.ToString("yyyy-MM")}/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = $"{now.ToString("dd")}_Quartz.txt";
                var fullName = path + fileName;

                var text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {logLevel.ToString()}:{messageFunc()}";
                Console.WriteLine(text, formatParameters);
                File.AppendAllText(fullName, text + Environment.NewLine, Encoding.UTF8);
            }
            return true;
        }


        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }
}
