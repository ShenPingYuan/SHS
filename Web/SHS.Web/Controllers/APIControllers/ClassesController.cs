using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SHS.Core;
using SHS.Dtos;
using SHS.Entities;
using SHS.IRepository;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        public ClassesController(IClassRepository classRepository,
            IMapper mapper,
            ITeacherRepository teacherRepository)
        {
            _classRepository = classRepository;
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetClasses()
        {
            var classes = await _classRepository.GetAllEntitiesAsIQueryable().Include(x => x.College).ToListAsync();
            var dtos = _mapper.Map<List<ClassDto>>(classes);
            for (int i = 0; i < dtos.Count(); i++)
            {
                dtos[i].InstructorName = (await _teacherRepository
                    .LoadEntitiesAsIQueryable(x => x.TeacherId == dtos[i].TeacherId)
                    .FirstOrDefaultAsync())?
                    .TeacherName;
            }
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "课程列表资源", dtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(int id)
        {
            var @class = await _classRepository.LoadEntitiesAsIQueryable(x => x.ClassId == id)
                .Include(x => x.College)
                .FirstOrDefaultAsync();
            if (@class == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<ClassDto>(@class);
            dto.InstructorName = (await _teacherRepository
                .LoadEntitiesAsIQueryable(x => x.TeacherId == dto.TeacherId)
                .FirstOrDefaultAsync()).TeacherName;
            return dto;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]ClassDto dto)
        {
            if (id != dto.ClassId)
            {
                return BadRequest();
            }
            var @class = await _classRepository.LoadEntitiesAsIQueryable(x => x.ClassId == id)
                .FirstOrDefaultAsync();
            if (@class == null)
            {
                return NotFound();
            }
            @class = _mapper.Map<Class>(dto);
            await _classRepository.EditEntityAsync(@class);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var @class =await _classRepository.LoadEntitiesAsIQueryable(x => x.ClassId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (@class == null)
            {
                return NotFound();
            }
            await _classRepository.DeleteEntityAsync(@class);
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> SearchClasss([FromQuery] int collegeid)
        {
            var classes =await _classRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == collegeid).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<ClassDto>>(classes);
            return Ok(dtos);
        }
    }
}