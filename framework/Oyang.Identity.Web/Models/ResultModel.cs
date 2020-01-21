using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.Web.Models
{
    public class ResultModel
    {
        public ResultModel()
        {
            Code = 0;
            Message = "操作成功";
        }
        public int Code { get; set; }
        public string Message { get; set; }

        public List<MemberError> ValidationErrors { get; set; }
    }

    public class ResultModel<T> : ResultModel
    {
        public ResultModel() : base()
        {

        }
        public T Data { get; set; }
    }


    public class MemberError
    {
        public string Member { get; set; }
        public string Message { get; set; }
    }

}
