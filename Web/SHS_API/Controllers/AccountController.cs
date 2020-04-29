using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHS.Entities;

namespace SHS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationIdentityUser> userManager
            , SignInManager<ApplicationIdentityUser> signInManager
            , RoleManager<ApplicationIdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new NullReferenceException("注入失败");
            _signInManager = signInManager ?? throw new NullReferenceException("注入失败");
            _roleManager = roleManager ?? throw new NullReferenceException("注入失败");
        }
    }
}