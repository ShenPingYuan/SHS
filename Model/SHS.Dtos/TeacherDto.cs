using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        /// <summary>
        /// 中文名字
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string UserFaceImgUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string UserDescription { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 课程名
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 学院名
        /// </summary>
        public string CollegeName { get; set; }
        /// <summary>
        /// 家庭地址/省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 家庭地址/市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 家庭地址/区
        /// </summary>
        public string Area { get; set; }
        public int? CourseId { get; set; }
        public int? ColleageId { get; set; }
    }
}
