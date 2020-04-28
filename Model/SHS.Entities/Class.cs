using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SHS.Entities
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }
        /// <summary>
        /// 班级号
        /// </summary>
        [Key]
        public int ClassId { get; set; }
        /// <summary>
        /// 班级名字
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 辅导员教职工号
        /// </summary>
        public int InstructorId { get; set; }
        /// <summary>
        /// 辅导员名字
        /// </summary>
        public string InstructorName { get; set; }
        public int CollegeId { get; set; }
        public College College { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
