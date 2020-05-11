using Microsoft.AspNetCore.Identity;
using SHS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Entities
{
    public class ApplicationIdentityRole:IdentityRole
    {
        public Position? Position{ get; set; }
    }
}
