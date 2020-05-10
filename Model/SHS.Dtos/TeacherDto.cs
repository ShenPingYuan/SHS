using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class TeacherDto
    {
        /// <summary>
        /// 教师编号
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// 中文名字
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string UserDescription { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Age { get; set; }
    }
}
