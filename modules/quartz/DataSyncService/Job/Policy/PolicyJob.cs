using DataSyncService.Core;
using DataSyncService.Core.Policy;
using DataSyncService.Infrastructure;
using DataSyncService.Infrastructure.Policy;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Job.Policy
{
    public class PolicyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var logger = new TextFileLogger();
            var policyConfig = new PolicyConfig();
            ITaskHandler taskHandler = new PolicyTaskHandler(logger, policyConfig);
            taskHandler.Process();

            //logger.Log(LogLevel.Debug, "测试");

            return Task.CompletedTask;
        }
    }
}
