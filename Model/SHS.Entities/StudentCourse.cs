using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Entities
{
    public partial class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double? Score { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
