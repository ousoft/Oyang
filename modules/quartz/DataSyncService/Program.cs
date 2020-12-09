using DataSyncService.Core;
using DataSyncService.Core.Policy;
using DataSyncService.Infrastructure;
using DataSyncService.Infrastructure.Policy;
using DataSyncService.Job;
using DataSyncService.Job.Policy;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Logging;

namespace DataSyncService
{
    class Program
    {
        static void Main(string[] args)
        {
            UseTopshelf();
            Console.WriteLine("ok");
        }

        private static void UseTopshelf()
        {
            var rc = HostFactory.Run(x =>
            {                
                x.Service<JobManager>(s =>
                {
                    s.ConstructUsing(name => new JobManager());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("丙肝筛查点小程序医保政策数据同步");
                x.SetDisplayName("Giead_HcvFindHospital");
                x.SetServiceName("Giead_HcvFindHospital");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
