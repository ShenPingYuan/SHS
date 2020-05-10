using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHS.Core;
using SHS.Dtos;
using SHS.Entities;
using SHS.IRepository;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        public TeachersController(UserManager<ApplicationIdentityUser> userManager,
            ITeacherRepository teacherRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetTeachers()
        {
            var teachers =await _teacherRepository.GetAllEntitiesAsIQueryable().ToListAsync();
            var dtos = _mapper.Map<IEnumerable<ListTeacherDto>>(teachers);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "教师列表", dtos);
        }
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<TeacherDto>> GetTeachers(int teacherId)
        {
            var teacher =await _teacherRepository.LoadEntitiesAsIQueryable(x => x.TeacherId == teacherId)
                .Include(x=>x.Course)
                .Include(x=>x.College)
                .FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            
            var dto = _mapper.Map<TeacherDto>(teacher);
            dto.CourseName = teacher.Course?.CourseName;
            dto.CollegeName = teacher.College?.CollegeName;
            return Ok(dto);
        }
        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<ResultData>> AddUser([FromBody]UserAddDto userDto)
        {
            if (userDto.UserName == null || userDto.Password == null)
            {
                return BadRequest();
            }
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                    return new ResultData(ReturnCode.Error, -1, "用户已存在", null);
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
                        return new ResultData(ReturnCode.Error, -1, "每天只能添加100个用户", null);
                    }
                }
                user = new ApplicationIdentityUser
                {
                    UserName = userDto.UserName,
                    Teacher = new Teacher
                    {
                        TeacherId = teacherId,
                    }
                };
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    return new ResultData(ReturnCode.Succeed, -1, "添加成功", teacherId);
                }
                return new ResultData(ReturnCode.Error, -1, "添加失败", null);
            }
        }
        // PUT: api/Teachers/5
        [HttpPut("{teacherId}")]
        public async Task<ActionResult<ResultData>> UpdateUser(int teacherId,[FromBody]TeacherDto dto)
        {
            if (teacherId != dto.TeacherId)
            {
                return BadRequest();
            }
            var teacher =await _teacherRepository.LoadEntitiesAsIQueryable(x => x.TeacherId == teacherId)
                .FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            teacher = _mapper.Map<Teacher>(dto);
            var result = await _teacherRepository.EditEntityAsync(teacher);
            if (result)
            {
                return new ResultData(ReturnCode.Succeed, -1, "更新成功", null);
            }
            return new ResultData(ReturnCode.Error, -1, "更新失败", null);
        }
        [HttpDelete("{teacherId}")]
        public async Task<ActionResult> Delete(int teacherId)
        {
            var teacher =await _teacherRepository.LoadEntitiesAsIQueryable(x => x.TeacherId == teacherId).FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            var result=await _teacherRepository.DeleteEntityAsync(teacher);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteList(List<int> teacherIds)
        {
            var teachers = await _teacherRepository
                .LoadEntitiesAsIQueryable(x => teacherIds.Contains(x.TeacherId))
                .ToListAsync();
            await _teacherRepository.DeleteEntitiesAsync(teachers);
            return Ok();
        }
    }
}
