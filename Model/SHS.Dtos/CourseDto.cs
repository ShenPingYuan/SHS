using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string EnglishName { get; set; }
        public string CourseScore { get; set; }
        public bool IsCompulsory { get; set; } = false;
    }
}
