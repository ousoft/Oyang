using DataSyncService.Job.Policy;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Job
{
    public class JobManager
    {
        private readonly IScheduler _scheduler;
        public JobManager()
        {
            LogProvider.SetCurrentLogProvider(new TextLogProvider());
            ISchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().Result;
        }
        public void Start() 
        {
            _scheduler.Start();
            _scheduler.Clear();
            _scheduler.AddPolicyJob();
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }
}
