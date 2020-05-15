using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SHS.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public int PositionGrade { get; set; }
        public string RoleName { get; set; }
    }
}
