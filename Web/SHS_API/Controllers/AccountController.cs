using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHS.Core;
using SHS.Dtos;
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
        [HttpPost]
        [Route("LoginApi")]
        public async Task<ActionResult<ResultData>> Login([FromBody]UserLoginDto userDto)
        {
            var user =await _userManager.FindByNameAsync(userDto.UserName);
            if (user == null)
            {
                return new ResultData(ReturnCode.Error, -1, "用户不存在", null);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, userDto.Password, false, false);
            if (result.Succeeded)
            {
                return new ResultData(ReturnCode.Succeed, -1, "登陆成功", null);
            }
            return new ResultData(ReturnCode.Error, -1, "密码错误", null);
        }
        [HttpPost]
        [Route("RegisterApi")]
        public async Task<ActionResult<ResultData>> Register([FromBody] UserRegisterDto userDto)
        {
            var user =await _userManager.FindByNameAsync(userDto.UserName);
            if (user != null)
            {
                return new ResultData(ReturnCode.Error, -1, "用户已经存在", null);
            }
            user = new ApplicationIdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };
            var result=await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                return new ResultData(ReturnCode.Succeed, -1, "注册成功", null);
            }
            return new ResultData(ReturnCode.Error, -1, "注册失败", null);
        }
        [HttpGet]
        [Route("LogOutApi")]
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}