using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataSyncService.Core.Policy
{
    public class PolicyModel
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 产品领域
        /// </summary>
        public string ProductArea { get; set; }
        /// <summary>
        /// 省份Id
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 省份编码
        /// </summary>
        public long ProvinceCode { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 城市编码
        /// </summary>
        public long CityCode { get; set; }
        /// <summary>
        /// 医保政策(Web)
        /// </summary>
        public string ContentWeb { get; set; }
        /// <summary>
        /// 医保政策(小程序)
        /// </summary>
        public string Content { get; set; }

        public static PolicyModel To(JToken jToken)
        {
            //创建时间 createTime  时间
            //修改时间    updateTime 时间
            //产品领域 productArea 关联 产品领域
            //省份 province    关联 省份
            //城市 city    关联 城市
            //医保政策(Web)   content_web 富文本
            //医保政策(小程序)   content 富文本
            //删除 isDelete    布尔值
            //产品  product 关联
            return new PolicyModel()
            {
                CreateTime = GetDateTime(jToken, "createTime"),
                UpdateTime = GetDateTime(jToken, "updateTime"),
                ProductArea = jToken.Value<string>("productArea"),
                ProvinceId = jToken.Value<string>("province"),
                CityId = jToken.Value<string>("city"),
                ContentWeb = jToken.Value<string>("content_web"),
                Content = jToken.Value<string>("content"),
            };
        }

        private static DateTime GetDateTime(JToken jToken, object key)
        {
            jToken = jToken.Value<JToken>(key);
            var ticket = jToken.Value<long>("$date");
            return new DateTime(ticket * 10000 + 621356256000000000);
        }
    }
}
