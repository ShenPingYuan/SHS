using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using SHS.Core;
using SHS.Dtos;
using SHS.Entities;
using SHS.IRepository;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        public readonly IMapper _mapper;
        public readonly ICollegeRepository _collegeRepository;
        public CollegesController(ICollegeRepository collegeRepository,
            IMapper mapper,
            ITeacherRepository teacherRepository)
        {
            _collegeRepository = collegeRepository;
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetColleges()
        {
            var colleges = await _collegeRepository.GetAllEntitiesAsIQueryable().ToListAsync();
            var dtos = _mapper.Map<List<CollegeDto>>(colleges);
            return new ResultData(ReturnCode.Succeed, -1, "学院列表", dtos);
        }
        [HttpGet("{collegeId}")]
        public async Task<ActionResult<CollegeDto>> GetCollege(int collegeId)
        {
            var college = await _collegeRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == collegeId).FirstOrDefaultAsync();
            if (college == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<CollegeDto>(college);
            return dto;
        }
        [HttpPost]
        public async Task<ActionResult> CreateCollege([FromBody]CollegeDto dto)
        {
            var college = await _collegeRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == dto.CollegeId||x.CollegeName==dto.CollegeName)
                .FirstOrDefaultAsync();
            if (college != null)
            {
                return BadRequest();
            }
            college = _mapper.Map<College>(dto);
            var collegeId =Convert.ToInt32(DateTime.Now.ToString("yyyyHHss"));
            college.CollegeId = collegeId;
            await _collegeRepository.AddEntityAsync(college);
            return NoContent();
        }
        [HttpDelete("{collegeId}")]
        public async Task<IActionResult> Delete(int collegeId)
        {
            var college = await _collegeRepository
                .LoadEntitiesAsIQueryable(x => x.CollegeId == collegeId)
                .FirstOrDefaultAsync();
            if (college == null)
            {
                return NotFound();
            }
            await _collegeRepository.DeleteEntityAsync(college);
            return NoContent();
        }
        [HttpPut("{collegeId}")]
        public async Task<ActionResult> Update(int collegeId,[FromBody]CollegeDto dto)
        {
            if (collegeId != dto.CollegeId)
            {
                return BadRequest();
            }
            var college =await _collegeRepository.LoadEntitiesAsIQueryable(x => x.CollegeId == dto.CollegeId).AsNoTracking().FirstOrDefaultAsync();
            if (college == null)
            {
                return BadRequest();
            }
            college = _mapper.Map<College>(dto);
            await _collegeRepository.EditEntityAsync(college);
            return NoContent();
        }
    }
}