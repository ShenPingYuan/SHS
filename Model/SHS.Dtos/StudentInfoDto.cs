﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class StudentInfoDto
    {
        /// <summary>
        /// 学号
        /// </summary>
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
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 学生头像链接
        /// </summary>
        public string UserFaceImgUrl { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// 班级Id
        /// </summary>
        public int? ClassId { get; set; }
        /// <summary>
        /// 班级名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
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
        public int CollegeId { get; set; }
        public string Age { get; set; }
    }
}
