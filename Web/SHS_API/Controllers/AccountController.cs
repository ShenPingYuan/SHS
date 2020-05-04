using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHS.Core;
using SHS.Data;
using SHS.Dtos;
using SHS.Entities;
using SHS.IRepository;

namespace SHS_API.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly ITeacherRepository _teacherRepository;
        public AccountController(UserManager<ApplicationIdentityUser> userManager
            , SignInManager<ApplicationIdentityUser> signInManager
            , RoleManager<ApplicationIdentityRole> roleManager
            , ITeacherRepository teacherRepository)
        {
            _userManager = userManager ?? throw new NullReferenceException(nameof(userManager) + "注入失败");
            _signInManager = signInManager ?? throw new NullReferenceException(nameof(signInManager) + "注入失败");
            _roleManager = roleManager ?? throw new NullReferenceException(nameof(roleManager) + "注入失败");
            _teacherRepository = teacherRepository ?? throw new NullReferenceException(nameof(teacherRepository) + "注入失败");
        }
        [HttpPost]
        [Route("LoginApi")]
        public async Task<ActionResult<ResultData>> Login([FromBody]UserLoginDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);
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
        public static string Text(int id)
        {
            if (id > 0)
            {
                id++;
            }
            id--;
            return Convert.ToString(id).Substring(0);
        }
        [HttpPost]
        [Route("RegisterApi")]
        public async Task<ActionResult<ResultData>> Register([FromBody] UserRegisterDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);
            if (user != null)
            {
                return new ResultData(ReturnCode.Error, -1, "用户已经存在", null);
            }
            string today = DateTime.Now.ToString("yyyyMMdd");
            var lastTeacher =_teacherRepository
                .LoadEntities(o => o.TeacherId.ToString().StartsWith(today))
                .LastOrDefault();
            int teacherId;
            
            if (lastTeacher == null)
            {
                teacherId = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd") + "00");
            }
            else
            {
                string lastTwoChar = lastTeacher.TeacherId.ToString().Substring(lastTeacher.TeacherId.ToString().Length - 2, 2);
                int index = Convert.ToInt32(lastTwoChar.StartsWith("0") ? lastTwoChar.Substring(1, 1) : lastTwoChar);
                if (index < 99)
                {
                    teacherId = index + 1;
                }
                else
                {
                    return new ResultData(ReturnCode.Error, -1, "今日注册已满", null);
                }
            }
            user = new ApplicationIdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Teacher = new Teacher
                {
                    TeacherId = teacherId
                }
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
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