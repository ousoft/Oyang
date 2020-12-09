﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Infrastructure
{
    public class AppConfigHelper
    {
        public static string GetAppSetting(string key)
        {
           return ConfigurationManager.AppSettings[key];
        }
    }
}
