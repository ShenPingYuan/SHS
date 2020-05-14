using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class StudentCourseDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
    }
}
