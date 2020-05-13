using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 班级名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        public string Age { get; set; }
    }
}
