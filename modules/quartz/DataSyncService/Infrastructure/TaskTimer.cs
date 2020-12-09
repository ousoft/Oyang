using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DataSyncService.Core;

namespace DataSyncService.Infrastructure
{
    public class TaskTimer
    {
        private readonly Timer _timer;

        public TaskTimer(int intervalSeconds, ITaskHandler taskHandler)
        {
            _timer = new Timer(intervalSeconds * 1000);
            _timer.Elapsed += (sender, eventArgs) => taskHandler.Process();
        }

        public void Start()
        {
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
