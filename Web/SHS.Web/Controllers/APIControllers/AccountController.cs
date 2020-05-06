using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHS.Core;
using SHS.Dtos;
using SHS.Entities;
using SHS.IRepository;

namespace SHS.Web.APIControllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        public AccountController(UserManager<ApplicationIdentityUser> userManager
            , SignInManager<ApplicationIdentityUser> signInManager
            , RoleManager<ApplicationIdentityRole> roleManager
            , ITeacherRepository teacherRepository
            , IMapper mapper
            , ICourseRepository courseRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
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
            var lastTeacher = _teacherRepository
                .LoadEntities(o => o.TeacherId.ToString().StartsWith(today))
                .LastOrDefault();
            int teacherId;

            if (lastTeacher == null)
            {
                teacherId = Convert.ToInt32(today + "00");
            }
            else
            {
                string lastTwoChar = lastTeacher.TeacherId.ToString().Substring(lastTeacher.TeacherId.ToString().Length - 2, 2);
                int index = Convert.ToInt32(lastTwoChar.StartsWith("0") ? lastTwoChar.Substring(1, 1) : lastTwoChar);
                if (index < 99)
                {
                    index++;
                    if (index.ToString().Length == 1)
                    {
                        teacherId = Convert.ToInt32(today + "0" + index.ToString());
                    }
                    else
                    {
                        teacherId = Convert.ToInt32(today + index.ToString());
                    }
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
                    TeacherId = teacherId,
                }
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                return new ResultData(ReturnCode.Succeed, -1, "注册成功", null);
            }
            return new ResultData(ReturnCode.Error, -1, "注册失败", null);
        }
        [HttpPost]
        [Route("LogOutApi")]
        public async Task LogOutApi()
        {
            await _signInManager.SignOutAsync();
        }
        [HttpGet]
        [Route("UserInfo/{userId}")]
        public async Task<ActionResult<UserInfoDto>> UserInfo(string userId)
        {
            ApplicationIdentityUser user = null;
            if (userId.ToLower() == "current")
            {
                user = await _userManager.GetUserAsync(HttpContext.User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
            }
            user = _userManager.Users.Where(x => x.Id == user.Id).Include(x => x.Teacher).AsEnumerable().FirstOrDefault();
            var teacher = user.Teacher;
            var Course = _courseRepository.LoadEntities(x => x.CourseId == teacher.CourseId).FirstOrDefault();
            var userRoles = await _userManager.GetRolesAsync(user);
            if (user != null && teacher != null)
            {
                UserInfoDto dto = new UserInfoDto
                {
                    UsersId = user.Id,
                    UserName = user.UserName,
                    TeacherId = teacher.TeacherId,
                    RealName = teacher.TeacherName,
                    Area = teacher.Area,
                    BirthDate = teacher.Birthday,
                    City = teacher.City,
                    CourseName = Course?.CourseName,
                    NickName = teacher.NickName,
                    EnglishName = teacher.EnglishName,
                    PhoneNumber = teacher.PhoneNumber,
                    Province = teacher.Province,
                    UserDesc = teacher.UserDescription,
                    UserEmail = teacher.UserEmail,
                    UserFaceImgUrl = teacher.UserFaceImgUrl,
                    UserGrades = userRoles,
                    UserSex = teacher.Sex
                };
                return dto;
            }
            return NotFound();
        }
    }
}