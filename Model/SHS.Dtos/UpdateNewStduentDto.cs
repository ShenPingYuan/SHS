using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SHS.Dtos
{
    public class UpdateNewStduentDto
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public string StudentName { get; set; }
        public string EnglishName { get; set; }
        public string Sex { get; set; }
        public string Password { get; set; }
    }
}
