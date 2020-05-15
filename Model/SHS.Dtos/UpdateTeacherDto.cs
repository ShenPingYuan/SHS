using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class UpdateTeacherDto
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int CollegeId { get; set; }
        public string RoleId { get; set; }
    }
}
