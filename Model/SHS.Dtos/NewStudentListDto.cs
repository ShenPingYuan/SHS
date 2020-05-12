using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class NewStudentListDto
    {
        public int StudentId { get; set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// 班级Id
        /// </summary>
        public string ClassName { get; set; }
    }
}
