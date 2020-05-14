using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class SCsController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IStudentRepository _studentRepository;
        public SCsController(IMapper mapper, IStudentCourseRepository studentCourseRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _studentCourseRepository = studentCourseRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetScs()
        {
            var scs = await _studentCourseRepository.GetAllEntitiesAsIQueryable()
                .Include(x => x.Student)
                .Include(x => x.Course).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<StudentCourseDto>>(scs);
            return new ResultData(ReturnCode.Succeed, scs.Count(), "选课信息", dtos);
        }
        [HttpGet("scores")]
        public async Task<ActionResult<ResultData>> GetScores()
        {
            var scs = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.Score != null)
                .Include(x => x.Student)
                .Include(x => x.Course).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<EntryScoreDto>>(scs);
            return new ResultData(ReturnCode.Succeed, scs.Count(), "成绩信息", dtos);
        }
        [HttpGet("searchstudents")]
        public async Task<ActionResult<IEnumerable<SimpleStudentDto>>> GetStudentsOfCourse([FromQuery]int courseId)
        {
            var scs = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == courseId)
                .Include(x => x.Student)
                .ToListAsync();
            var dtos = _mapper.Map<IEnumerable<SimpleStudentDto>>(scs);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<ActionResult> SelectCourse([FromBody]SelectCourseDto dto)
        {
            var course = await _courseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == dto.CourseId).FirstOrDefaultAsync();
            if (course == null)
            {
                return BadRequest();
            }
            var students = await _studentRepository.LoadEntitiesAsIQueryable(x => x.ClassId == dto.ClassId).ToListAsync();
            foreach (var item in students)
            {
                StudentCourse sc = new StudentCourse
                {
                    Course = course,
                    Student = item,
                };
                var dbsc = await _studentCourseRepository
                    .LoadEntitiesAsIQueryable(x => x.CourseId == course.CourseId && x.StudentId == item.StudentId)
                    .FirstOrDefaultAsync();
                if (dbsc != null)
                {
                    return BadRequest();
                }
                await _studentCourseRepository.AddEntityAsync(sc);
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteRange([FromForm]List<int> courseIds, [FromForm]List<int> studentIds)
        {
            if (courseIds.Count() != studentIds.Count())
            {
                return BadRequest();
            }
            for (int i = 0; i < courseIds.Count(); i++)
            {
                var sc = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == courseIds[i] && x.StudentId == studentIds[i])
                    .FirstOrDefaultAsync();
                if (sc == null)
                {
                    return NotFound();
                }
                await _studentCourseRepository.DeleteEntityAsync(sc);
            }
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> EntryScore(EntryScoreDto dto)
        {
            var sc = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == dto.CourseId && x.StudentId == dto.StudentId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (sc == null)
            {
                return NotFound();
            }
            sc.Score = dto.Score;
            await _studentCourseRepository.EditEntityAsync(sc);
            return NoContent();
        }
        [HttpDelete("scores")]
        public async Task<ActionResult> DeleteScores([FromForm]List<int> courseIds, [FromForm]List<int> studentIds)
        {
            if (courseIds.Count() != studentIds.Count())
            {
                return BadRequest();
            }
            for (int i = 0; i < courseIds.Count(); i++)
            {
                var sc = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == courseIds[i] && x.StudentId == studentIds[i])
                    .FirstOrDefaultAsync();
                if (sc == null)
                {
                    return NotFound();
                }
                sc.Score = null;
                await _studentCourseRepository.EditEntityAsync(sc);
            }
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<ResultData>> SearchScore([FromQuery]int collegeId, [FromQuery]int classId, 
            [FromQuery]int studentId, [FromQuery]int courseId)
        {
            List<StudentCourse> scs;
            if (collegeId != 0)
            {
                scs = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.Student.Class.CollegeId == collegeId)
                    .Include(x=>x.Student).Include(x=>x.Course)
                    .ToListAsync();
            }
            else if (classId != 0)
            {
                scs = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.Student.ClassId == classId)
                    .Include(x => x.Student).Include(x => x.Course).ToListAsync();
            }
            else if (studentId != 0)
            {
                scs = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.StudentId == studentId)
                    .Include(x => x.Student).Include(x => x.Course).ToListAsync();
            }
            else if (courseId != 0)
            {
                scs = await _studentCourseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == courseId)
                    .Include(x => x.Student).Include(x => x.Course).ToListAsync();
            }
            else
            {
                scs = await _studentCourseRepository.GetAllEntitiesAsIQueryable()
                    .Include(x => x.Student).Include(x => x.Course).ToListAsync();
            }
            var dtos = _mapper.Map<IEnumerable<EntryScoreDto>>(scs);
            return new ResultData(ReturnCode.Succeed,dtos.Count(),"查询结果",dtos);
        }
    }
}