using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SHS.Entities
{
    /// <summary>
    /// 学院
    /// </summary>
    public class College
    {
        public College()
        {
            Teachers = new HashSet<Teacher>();
            Classes = new HashSet<Class>();
        }
        /// <summary>
        /// 学院编号
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
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
        public int? DeanId { get; set; }
        /// <summary>
        /// 院长名字
        /// </summary>
        public string DeanName { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
