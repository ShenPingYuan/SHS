using System;
using System.Collections.Generic;

namespace SHS.Core
{
    public class ResultData
    {
        public ResultData(ReturnCode code, int count, string msg, IEnumerable<object> datas)
        {
            Data = datas;
            Code = code;
            Count = count;
            Msg = msg;
        }
        public ResultData(ReturnCode code, int count, string msg, object data)
        {
            Code = code;
            Count = count;
            Msg = msg;
            Data = data;
        }
        /// <summary>
        /// 返回码
        /// </summary>
        public ReturnCode Code { get; set; }
        /// <summary>
        /// 返回数据数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }
    }
}
