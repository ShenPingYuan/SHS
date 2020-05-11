﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHS.Entities;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        public RolesController(RoleManager<ApplicationIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}