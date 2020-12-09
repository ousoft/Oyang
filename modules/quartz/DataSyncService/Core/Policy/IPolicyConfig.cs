using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Core.Policy
{
    public interface IPolicyConfig
    {
        string CloudBaseEnvId { get; }
        string CloudBaseTicketUrl { get; }
        string ConnectionString { get; }
    }
}
