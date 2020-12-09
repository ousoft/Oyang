using CloudBase;
using DataSyncService.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncService.Core.Policy
{
    /// <summary>
    /// 医保政策处理
    /// </summary>
    public class PolicyTaskHandler : ITaskHandler
    {
        private readonly ILogger _logger;
        private readonly IPolicyConfig _policyConfig;
        private readonly CloudBaseApp _cloudBaseApp;
        private readonly HttpClient _httpClient;

        private List<PolicyModel> _list;

        public PolicyTaskHandler(
            ILogger logger,
            IPolicyConfig policyConfig
            )
        {
            _logger = logger;
            _policyConfig = policyConfig;
            _cloudBaseApp = CloudBaseApp.Init(_policyConfig.CloudBaseEnvId, 10000);
            _httpClient = new HttpClient();
        }

        public void Process()
        {
            try
            {
                WriteLog(LogLevel.Information, "==================================================");

                WriteLog(LogLevel.Information, $"{nameof(ReadRemoteData)} Start");
                ReadRemoteData();
                WriteLog(LogLevel.Information, $"{nameof(ReadRemoteData)} End");

                WriteLog(LogLevel.Information, $"{nameof(WriteToLocalDatabase)} Start");
                WriteToLocalDatabase();
                WriteLog(LogLevel.Information, $"{nameof(WriteToLocalDatabase)} End");

                WriteLog(LogLevel.Information, $"{_list.Count}条HCV医保政策数据同步成功");
            }
            catch (Exception e)
            {
                string message = null;
                GetInnerException(ref message, e);
                WriteLog(LogLevel.Error, message);
            }
            finally
            {
                WriteLog(LogLevel.Information, "==================================================");
            }
        }

        /// <summary>
        /// 从CloudBase获取医保政策相关数据
        /// </summary>
        private void ReadRemoteData()
        {
            //云开发登录鉴权
            AuthState authState = _cloudBaseApp.Auth.GetAuthStateAsync().Result;
            if (authState == null)
            {
                if (!string.IsNullOrWhiteSpace(_policyConfig.CloudBaseTicketUrl))
                {
                    //Ticket登录
                    var ticket =  _httpClient.GetStringAsync(_policyConfig.CloudBaseTicketUrl).Result;
                    authState = _cloudBaseApp.Auth.SignInWithTicketAsync(ticket).Result;
                }
                else
                {
                    //匿名登录
                    authState = _cloudBaseApp.Auth.SignInAnonymouslyAsync().Result;
                }
            }

            //获取HCV医保政策数据
            var query = _cloudBaseApp.Db.Collection("policy").Where(new
            {
                productArea = _cloudBaseApp.Db.Command.Eq("9ffb2a485f323e8d005ecf38609ce572"),
                isDelete = _cloudBaseApp.Db.Command.Eq(false)
            });
            var count = query.Count();
            var dbQueryResponse = query.Limit(count.Total).Get();
            if (!string.IsNullOrWhiteSpace(dbQueryResponse.Code))
            {
                throw new Exception(dbQueryResponse.Message);
            }
            //WriteLog(LogLevel.Debug, JsonConvert.SerializeObject(dbQueryResponse));
            _list = dbQueryResponse.Data.Select(t => PolicyModel.To(t)).ToList();

            //获取省份
            dbQueryResponse = _cloudBaseApp.Db.Collection("province").Limit(100).Get();
            if (!string.IsNullOrWhiteSpace(dbQueryResponse.Code))
            {
                throw new Exception(dbQueryResponse.Message);
            }
            var listProvince = dbQueryResponse.Data.Select(t => new ProvinceModel()
            {
                Id = t.Value<string>("_id"),
                Name = t.Value<string>("province"),
                Code = t.Value<long>("code"),
            }).ToList();

            //获取城市
            dbQueryResponse = _cloudBaseApp.Db.Collection("city").Limit(1000).Get();
            if (!string.IsNullOrWhiteSpace(dbQueryResponse.Code))
            {
                throw new Exception(dbQueryResponse.Message);
            }

            var listCity = dbQueryResponse.Data.Select(t => new CityModel()
            {
                Id = t.Value<string>("_id"),
                Name = t.Value<string>("city"),
                Code = t.Value<long>("code"),
            }).ToList();

            foreach (var item in _list)
            {
                var province = listProvince.Single(t => t.Id == item.ProvinceId);
                item.ProvinceName = province.Name;
                item.ProvinceCode = province.Code;
                var city = listCity.Single(t => t.Id == item.CityId);
                item.CityName = city.Name;
                item.CityCode = city.Code;
            }

            _cloudBaseApp.Auth.SignOutAsync();
        }

        /// <summary>
        /// 将医保政策数据写入本地数据库
        /// </summary>
        private void WriteToLocalDatabase()
        {
            if (_list == null || _list.Count <= 0)
            {
                return;
            }
            using (var connection = new SqlConnection(_policyConfig.ConnectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM [Policy]";
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection))
                {
                    dataAdapter.UpdateBatchSize = 50;
                    new SqlCommandBuilder(dataAdapter);
                    var nowTime = DateTime.Now;

                    var dt = new DataTable();
                    dataAdapter.Fill(dt);

                    //删除历史数据
                    foreach (var item in dt.Select("IsDeleted = true"))
                    {
                        item.Delete();
                    }

                    //软删除现有数据
                    foreach (DataRow item in dt.Select("IsDeleted = false"))
                    {
                        item["DeletionTime"] = nowTime;
                        item["IsDeleted"] = true;
                    }

                    //添加新数据
                    var newTable = GenerateDataTable(nowTime);
                    dt.Merge(newTable);

                    //更新到数据库
                    dataAdapter.Update(dt);
                }
            }
        }

        private DataTable GenerateDataTable(DateTime nowTime)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("CreateTime", typeof(DateTime));
            dt.Columns.Add("UpdateTime", typeof(DateTime));
            dt.Columns.Add("ProductArea", typeof(string));
            dt.Columns.Add("ProvinceId", typeof(string));
            dt.Columns.Add("ProvinceName", typeof(string));
            dt.Columns.Add("ProvinceCode", typeof(long));
            dt.Columns.Add("CityId", typeof(string));
            dt.Columns.Add("CityName", typeof(string));
            dt.Columns.Add("CityCode", typeof(long));
            dt.Columns.Add("ContentWeb", typeof(string));
            dt.Columns.Add("Content", typeof(string));
            dt.Columns.Add("SyncTime", typeof(DateTime));
            dt.Columns.Add("DeletionTime", typeof(DateTime));
            dt.Columns.Add("IsDeleted", typeof(bool));
            foreach (var item in _list)
            {
                var row = dt.NewRow();

                row["Id"] = Guid.NewGuid();
                row["CreateTime"] = item.CreateTime;
                row["UpdateTime"] = item.UpdateTime;
                row["ProductArea"] = item.ProductArea;
                row["ProvinceId"] = item.ProvinceId;
                row["ProvinceName"] = item.ProvinceName;
                row["ProvinceCode"] = item.ProvinceCode;
                row["CityId"] = item.CityId;
                row["CityName"] = item.CityName;
                row["CityCode"] = item.CityCode;
                row["ContentWeb"] = item.ContentWeb;
                row["Content"] = item.Content;
                row["SyncTime"] = nowTime;
                row["DeletionTime"] = DBNull.Value;
                row["IsDeleted"] = false;

                dt.Rows.Add(row);
            }
            return dt;
        }

        private void WriteLog(LogLevel logLevel, string content, bool isSendNotification = false)
        {
            _logger.Log(logLevel, $"[PolicyTaskHandler] {content}");
            if (isSendNotification)
            {
                //通知监控平台

            }
        }

        private void GetInnerException(ref string message, Exception e)
        {
            message += e.Message + Environment.NewLine;
            if (e.InnerException != null)
            {
                GetInnerException(ref message, e.InnerException);
            }
        }
    }
}
