using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Entities
{
    public partial class Student_Course
    {
        public int Sno { get; set; }
        public int Cno { get; set; }
        public string Score { get; set; }
        public int Tid { get; set; }
        public virtual Course CourseNavigation { get; set; }
        public virtual Student StudentNavigation { get; set; }
    }
}
