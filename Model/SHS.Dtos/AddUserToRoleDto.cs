using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class AddUserToRoleDto
    {
        public string RoleId { get; set; }
        public int TeacherId { get; set; }
    }
}
