using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Entities
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
