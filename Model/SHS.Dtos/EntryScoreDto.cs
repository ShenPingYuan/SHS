using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace SHS.Dtos
{
    public class EntryScoreDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public float Score { get; set; }
    }
}
