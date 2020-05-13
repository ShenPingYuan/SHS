using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ICollegeRepository _collegeRepository;
        private readonly IStudentRepository _studentRepository;
        public StudentsController(IStudentRepository studentRepository,
            ICollegeRepository collegeRepository,
            IMapper mapper,
            IClassRepository classRepository)
        {
            _collegeRepository = collegeRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _mapper = mapper;
        }
        [HttpGet("newstudents")]
        public async Task<ActionResult<ResultData>> GetNewStudents()
        {
            var students =await _studentRepository.LoadEntitiesAsIQueryable(x => x.Year == DateTime.Now.Year)
                .Include(x=>x.Class)
                .ToListAsync();
            var dtos = _mapper.Map<IEnumerable<NewStudentListDto>>(students);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "添加新生", dtos);
        }
        [HttpGet("newstudentlist")]
        public async Task<ActionResult<ResultData>> GetNewStudentList()
        {
            var students = await _studentRepository.LoadEntitiesAsIQueryable(x => x.Year == DateTime.Now.Year)
                .Include(x => x.Class)
                .ToListAsync();
            var dtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "新生列表", dtos);
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetStudents()
        {
            var students = await _studentRepository.GetAllEntitiesAsIQueryable()
                .Include(x => x.Class)
                .ToListAsync();
            var dtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "学生列表", dtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentInfoDto>> GetStudent(int id)
        {
            var student =await _studentRepository.LoadEntitiesAsIQueryable(x => x.StudentId == id)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            return _mapper.Map<StudentInfoDto>(student);
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]StudentAddDto dto)
        {
            var college = await _collegeRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == dto.CollegeId)
                .Include(x => x.Classes)
                .FirstOrDefaultAsync();
            if (college == null)
            {
                return BadRequest();
            }
            var classCount = college.Classes.Count();
            var @class1 = _classRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == dto.CollegeId)
                .Include(x => x.Students);
            var @class = await _classRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == dto.CollegeId)
                .Include(x => x.Students).OrderBy(x => x.Students.Count()).FirstOrDefaultAsync();
            if (@class == null || @class.Students.Count() > 50)
            {
                int classId = Convert.ToInt32(dto.CollegeId.ToString()
                    .Substring(dto.CollegeId.ToString().Length-2,2)+(college.Classes.Count()+1));
                @class = new Class
                {
                    ClassId = classId,
                    ClassName = college.CollegeName + (classCount + 1) + "班",
                    College = college,
                };
            }
            int studentCount = @class.Students.Count();
            var lastTwoOfClass = "";
            var lastTwoOfStudent = "";
            if (studentCount.ToString().Length >= 2)
            {
                lastTwoOfStudent = studentCount.ToString().Substring(studentCount.ToString().Length - 2);
            }
            else
            {
                lastTwoOfStudent = "0" + studentCount.ToString();
            }
            if (classCount.ToString().Length >= 2)
            {
                lastTwoOfClass = classCount.ToString().Substring(classCount.ToString().Length - 2);
            }
            else
            {
                lastTwoOfClass = "0" + classCount.ToString();
            }
            var strstudentId = DateTime.Now.Year.ToString().Substring(2,2) + college.CollegeId.ToString()
                .Substring(college.CollegeId.ToString().Length - 3) + lastTwoOfClass + lastTwoOfStudent;
            var studentId = Convert.ToInt32(strstudentId);
            Student student = new Student
            {
                StudentId = studentId,
                StudentName = dto.StudentName,
                Class = @class,
                Year = DateTime.Now.Year,
                Password = studentId.ToString(),
            };
            await _studentRepository.AddEntityAsync(student);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _studentRepository.LoadEntitiesAsIQueryable(x => x.StudentId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteEntityAsync(student);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,[FromBody]StudentInfoDto dto)
        {
            if (id != dto.StudentId)
            {
                return BadRequest();
            }
            var student = await _studentRepository.LoadEntitiesAsIQueryable(x => x.StudentId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            student = _mapper.Map<Student>(dto);
            await _studentRepository.EditEntityAsync(student);
            return NoContent();
        }
        [HttpPost("one")]
        public async Task<ActionResult> CreateAstudent([FromBody]StudentAddDto dto)
        {
            var @class =await _classRepository.LoadEntitiesAsIQueryable(x => x.ClassId == dto.ClassId).FirstOrDefaultAsync();
            var college=await _collegeRepository.LoadEntitiesAsIQueryable(x=>x.CollegeId==dto.CollegeId).FirstOrDefaultAsync();
            if (@class == null||college==null)
            {
                return NotFound();
            }
            int studentId = Convert.ToInt32(DateTime.Now.ToString("yyyyMMSS") + DateTime.Now.Day.ToString().Substring(DateTime.Now.Day.ToString().Length - 1));

            Student student = new Student
            {
                StudentId = studentId,
                StudentName = dto.StudentName,
                Class = @class,
                Password = studentId.ToString(),
                Year = DateTime.Now.Year,
            };
            await _studentRepository.AddEntityAsync(student);
            return NoContent();
        }
    }
}