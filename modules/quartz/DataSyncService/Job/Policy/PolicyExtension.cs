using DataSyncService.Infrastructure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Job.Policy
{
    public static class PolicyExtension
    {
        public static void AddPolicyJob(this IScheduler scheduler)
        {
            var jobName = "Giead_HcvFindHospital";

            IJobDetail jobDetail = JobBuilder.Create<PolicyJob>()
                .WithIdentity(jobName, "丙肝筛查点小程序医保政策数据同步")
                .Build();

            var triggerName = $"{jobName}_Trigger";
            var cronExpression = AppConfigHelper.GetAppSetting(triggerName);
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerName)
                //.WithCronSchedule(cronExpression)
                .WithSimpleSchedule(t => t.WithIntervalInSeconds(60*60))

                .Build();

            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
