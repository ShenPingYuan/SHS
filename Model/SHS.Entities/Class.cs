using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SHS.Entities
{
    public partial class Class
    {
        public Class()
        {
            TbStudent = new HashSet<Student>();
        }
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Dean { get; set; }

        public virtual ICollection<Student> TbStudent { get; set; }
    }
}
