using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Entities
{
    public partial class Course
    {
        public Course()
        {
            Sc = new HashSet<Student_Course>();
            Teachers = new HashSet<Teacher>();
        }

        public int Cno { get; set; }
        public string Cname { get; set; }
        public string EnglishName { get; set; }
        public string CScore { get; set; }
        public virtual ICollection<Student_Course> Sc { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
