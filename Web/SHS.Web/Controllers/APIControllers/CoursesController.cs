using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SHS.Core;
using SHS.Dtos;
using SHS.Entities;
using SHS.IRepository;

namespace SHS.Web.Controllers.APIControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        public CoursesController(ICourseRepository courseRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetCoursesAsync()
        {
            var courses = await _courseRepository.GetAllEntitiesAsIQueryable().ToListAsync();
            var dtos = _mapper.Map<List<CourseDto>>(courses);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "课程列表", dtos);
        }
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourseAsync(int courseId)
        {
            var course =await _courseRepository.LoadEntitiesAsIQueryable(x => x.CourseId == courseId).FirstOrDefaultAsync();
            if (course == null)
            {
                return BadRequest();
            }
            return _mapper.Map<CourseDto>(course);
        }
        [HttpPut("{courseId}")]
        public async Task<ActionResult> Update(int courseId,[FromBody]CourseDto dto)
        {
            if (courseId != dto.CourseId)
            {
                return BadRequest();
            }
            var course =await _courseRepository
                .LoadEntitiesAsIQueryable(x => x.CourseId == courseId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            course = _mapper.Map<Course>(dto);
            await _courseRepository.EditEntityAsync(course);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody]CourseDto dto)
        {
            var course =await _courseRepository.LoadEntitiesAsIQueryable(x => x.CourseName == dto.CourseName).FirstOrDefaultAsync();
            if (course != null)
            {
                return BadRequest();
            }
            course = _mapper.Map<Course>(dto);
            int couresId = Convert.ToInt32(DateTime.Now.ToString("MMddss"));
            course.CourseId = couresId;
            await _courseRepository.AddEntityAsync(course);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var course =await _courseRepository
                .LoadEntitiesAsIQueryable(x => x.CourseId == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return NotFound();
            }
            await _courseRepository.DeleteEntityAsync(course);
            return NoContent();
        }

    }
}