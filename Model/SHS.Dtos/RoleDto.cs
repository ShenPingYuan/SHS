using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SHS.Dtos
{
    public class RoleDto
    {
        [Required]
        public int PositionGrade { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
