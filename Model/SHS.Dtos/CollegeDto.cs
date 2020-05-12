using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class CollegeDto
    {
        public int CollegeId { get; set; }
        /// <summary>
        /// 学院名称
        /// </summary>
        public string CollegeName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 院长教师公号
        /// </summary>
        public int? TeacherId { get; set; }
        /// <summary>
        /// 院长名字
        /// </summary>
        public string DeanName { get; set; }
    }
}
