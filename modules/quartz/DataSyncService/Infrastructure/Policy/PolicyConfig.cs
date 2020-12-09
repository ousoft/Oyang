using DataSyncService.Core.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Infrastructure.Policy
{
    public class PolicyConfig : IPolicyConfig
    {
        public PolicyConfig()
        {
            //CloudBaseEnvId = "env-pnqsaynl";
            //ConnectionString = "Data Source=172.31.4.133;Initial Catalog=DB_Hospital;User ID=zwang;Password=gSs315@M";
            CloudBaseEnvId = AppConfigHelper.GetAppSetting(nameof(CloudBaseEnvId));
            CloudBaseTicketUrl = AppConfigHelper.GetAppSetting(nameof(CloudBaseTicketUrl));
            ConnectionString = AppConfigHelper.GetAppSetting(nameof(ConnectionString));
        }

        public string CloudBaseEnvId { get; }

        public string CloudBaseTicketUrl { get; }
        public string ConnectionString { get; }
    }
}
