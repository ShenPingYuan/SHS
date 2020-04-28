using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SHS.Entities
{
    public partial class Course
    {
        public Course()
        {
            StudentCourse = new HashSet<Student_Course>();
            Teachers = new HashSet<Teacher>();
        }
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string EnglishName { get; set; }
        public string CourseScore { get; set; }
        public virtual ICollection<Student_Course> StudentCourse { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
