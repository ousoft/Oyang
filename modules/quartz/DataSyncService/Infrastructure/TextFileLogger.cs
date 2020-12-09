using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncService.Core;

namespace DataSyncService.Infrastructure
{
    public class TextFileLogger : ILogger
    {
        public void Log(LogLevel logLevel, string content)
        {
            var now = DateTime.Now;
            var path = $@"logs/{now.ToString("yyyy-MM")}/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = $"{now.ToString("dd")}.txt";
            var fullName = path + fileName;

            var text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {logLevel.ToString()}:{content}";
            Console.WriteLine(text);
            File.AppendAllText(fullName, text + Environment.NewLine, Encoding.UTF8);
        }
    }
}
