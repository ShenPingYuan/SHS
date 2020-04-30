using System;
using System.Collections.Generic;

namespace SHS.Core
{
    public class ResultData
    {
        public ResultData(ReturnCode code, int count, string msg, IEnumerable<object> data)
        {
            Data = new List<object>();
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
        /// 返回的实际数据
        /// </summary>
        public IEnumerable<object> Data { get; set; }
    }
}
