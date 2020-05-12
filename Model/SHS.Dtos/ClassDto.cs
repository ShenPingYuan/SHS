using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class ClassDto
    {
        public int ClassId { get; set; }
        /// <summary>
        /// 班级名字
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 辅导员教职工号
        /// </summary>
        public int? TeacherId { get; set; }
        /// <summary>
        /// 辅导员名字
        /// </summary>
        public string InstructorName { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
    }
}
